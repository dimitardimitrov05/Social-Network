using Connectly.Contracts;
using Connectly.Data.Account;
using Connectly.Models.FriendshipViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Connectly.Controllers
{
    public class FriendshipsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFriendshipService _friendshipService;

        public FriendshipsController(UserManager<User> userManager, IFriendshipService friendshipService)
        {
            _userManager = userManager;
            _friendshipService = friendshipService;
        }

        public async Task<IActionResult> SendFriendship(string id)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);
            var model = new SendFriendshipViewModel()
            {
                SenderId = currentUser.Id,
                ReceiverId = id
            };

            await _friendshipService.SendFriendshipAsync(model);
            return RedirectToAction("ProfileInfo", "Account", new { id = model.ReceiverId });
        }

        public async Task<IActionResult> AcceptRequest(string id)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            await _friendshipService.AcceptFriendRequestAsync(currentUser.Id, id);
            return RedirectToAction("ProfileInfo", "Account", new { id = id });
        }

        public async Task<IActionResult> DeclineRequest(string id)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            await _friendshipService.DeclineFriendRequestAsync(currentUser.Id, id);
            return RedirectToAction("ProfileInfo", "Account", new { id = id });
        }

        public async Task<IActionResult> RemoveFriendship(string id)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            await _friendshipService.DeleteFriendshipAsync(currentUser.Id, id);
            return RedirectToAction("ProfileInfo", "Account", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> FriendsOfUser()
        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            var friends = await _friendshipService.FriendsOfUserAsync(currentUser.Id);
            var model = new IndexFriendsViewModel()
            {
                Friends = friends,
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FriendRequests()
        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            var requests = await _friendshipService.ListFriendRequestsAsync(currentUser.Id);
            var model = new List<FriendRequestsViewModel>();
            model.AddRange(requests);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SentRequests()
        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            var requests = await _friendshipService.ListSentFriendRequestsAsync(currentUser.Id);
            var model = new List<SentRequestsViewModel>();
            model.AddRange(requests);
            return View(model);
        }
    }
}
