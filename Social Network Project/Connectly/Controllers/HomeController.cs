using Connectly.Contracts;
using Connectly.Data.Account;
using Connectly.Models;
using Connectly.Models.Pagination;
using Connectly.Models.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tls;
using System.Diagnostics;

namespace Connectly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IPostService postService)
        {
            _logger = logger;
            _userManager = userManager;
            _postService = postService;
        }

        public async Task<IActionResult> Index(int pageNumber)
        {
            if (User?.Identity?.IsAuthenticated is false)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.GetUserAsync(this.User);
            var posts = await _postService.ListPostsAsync(user.Id);
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 25;

            var model = new IndexViewModel()
            {
                CurrentUserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.Image,
                Posts = await PaginatedList<PostViewModel>.CreateAsync(posts, pageNumber, pageSize)
            };
            var EmailError = TempData["EmailError"] as string;
            var nullPostError = TempData["NullPostError"] as string;
            var nullVisibilityError = TempData["NullVisibilityError"] as string;

            ViewBag.EmailError = EmailError;
            ViewBag.NullPostError = nullPostError;
            ViewBag.NullVisibilityError = nullVisibilityError;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
