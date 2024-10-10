using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Models.AccountViewModels;
using Connectly.Models.FriendshipViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IFriendshipService _friendshipService;
        private readonly IPostService _postService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context, IFriendshipService friendshipService, IPostService postService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _friendshipService = friendshipService;
            _postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var findInvite = await _context.Invitations.Where(x => x.UserRegistratedFromInvite == model.EmailAddress).FirstOrDefaultAsync();
            if (!ModelState.IsValid || findInvite == null || findInvite.VerificationCode != model.VerificationCode || DateTime.Now > findInvite.ExpirationOfInvite)
            {
                return View(model);
            }
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.EmailAddress,
                UserName = model.Username,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                AccountPrivacy = model.AccountPrivacy,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var friendshipModel = new CreateFriendshipFromAcceptedInvitationViewModel()
                {
                    UserAcceptedFriendship = user.Id,
                    UserSendedFriendship = findInvite.UserCreatedTheInvite
                };
                await _friendshipService.CreateFriendshipAsync(friendshipModel);
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Login", "Account");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileInfo(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException("There isn't user with this id");
            }

            var currentUser = await _userManager.GetUserAsync(this.User);

            if (id == currentUser.Id)
            {
                return RedirectToAction("CurrentUserProfile");
            }

            var model = new UserProfileViewModel()
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                AccountPrivacy = user.AccountPrivacy,
                Image = user.Image,
                IsFriendWithCurrentUser = await _friendshipService.IsFriendAsync(currentUser.Id, user.Id),
                Posts = await _postService.UserPostsAsync(currentUser.Id, id),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CurrentUserProfile()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var model = new CurrentUserProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                AccountPrivacy = user.AccountPrivacy,
                Image = user.Image,
                Posts = await _postService.CurrentUserPostsAsync(user.Id),
            };

            return View(model);
        }
    }
}
