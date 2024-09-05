using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Models.PostViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Connectly.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        public PostsController(IPostService postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(IndexViewModel model)
        {
            if (model.PostContent == null || model.PostVisibility == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.GetUserAsync(this.User);
            await _postService.CreatePostAsync(model, user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
