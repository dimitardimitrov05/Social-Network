using Connectly.Contracts;
using Connectly.Data.Account;
using Connectly.Models;
using Connectly.Models.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            if (User?.Identity?.IsAuthenticated is false)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.GetUserAsync(this.User);
            var model = new IndexViewModel()
            {
                CurrentUserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePicture = user.Image,
                Posts = await _postService.ListPosts(user.Id)
            };
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
