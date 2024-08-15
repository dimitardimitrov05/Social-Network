using Connectly.Contracts;
using Connectly.Data.Account;
using Connectly.Models.InvitationViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Connectly.Controllers
{
    [Authorize]
    public class InvitationsController : Controller
    {
        private readonly IInvitationService invitationService;
        private readonly UserManager<User> userManager;

        public InvitationsController(IInvitationService invitationService, UserManager<User> userManager)
        {
            this.invitationService = invitationService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateInviteViewModel model)
        {   
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await userManager.GetUserAsync(this.User);
            await invitationService.CreateIvitationAsync(model, currentUser);

            return RedirectToAction("Index", "Home");
        }
    }
}
