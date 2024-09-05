using Connectly.Contracts;
using Connectly.Data.Account;
using Connectly.Models.FriendshipViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Connectly.Controllers
{
    [Authorize]
    public class FriendshipsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFriendshipService _friendshipService;

        public FriendshipsController(UserManager<User> userManager, IFriendshipService friendshipService)
        {
            _userManager = userManager;
            _friendshipService = friendshipService;
        }

        [HttpPost]
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

            await _friendshipService.AcceptFriendRequest(currentUser.Id, id);
            return RedirectToAction("ProfileInfo", "Account", new { id = id });
        }

        public async Task<IActionResult> DeclineRequest(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(this.User);

            await _friendshipService.DeclineFriendRequest(currentUser.Id, userId);
            return RedirectToAction("ProfileInfo", "Account", new { id = userId });
        }
    }
}
