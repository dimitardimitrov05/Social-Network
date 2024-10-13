using Connectly.Contracts;
using Connectly.Data.Account;
using Connectly.Models.InvitationViewModels;
using Connectly.Models.Pagination;
using Connectly.Models.PostViewModels;
using Connectly.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Connectly.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly IInvitationService _invitationService;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public InvitationsController(IInvitationService invitationService, UserManager<User> userManager, IUserRepository userRepository)
        {
            _invitationService = invitationService;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(IndexViewModel model)
        {   
            if (model.EmailOfReceiver == null)
            {
                TempData["EmailError"] = "Please write the email of your friend!";
                return RedirectToAction("Index", "Home", model);
            }
            var registratedUser = _userRepository.IsTherUserWithThisEmail(model.EmailOfReceiver);
            if (registratedUser)
            {
                TempData["EmailError"] = "There is already user with this email";
                return RedirectToAction("Index", "Home", model);
            }

            var currentUser = await _userManager.GetUserAsync(this.User);
            await _invitationService.CreateIvitationAsync(model, currentUser);

            return RedirectToAction("Index", "Home");
        }
    }
}
