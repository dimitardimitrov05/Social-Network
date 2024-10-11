using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Data.Entities;
using Connectly.Models.InvitationViewModels;
using Connectly.Models.PostViewModels;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IEmailSender _emailSender;

        public InvitationService(IUserRepository userRepository, IInvitationRepository invitationRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _invitationRepository = invitationRepository;
            _emailSender = emailSender;
        }

        public async Task CreateIvitationAsync(IndexViewModel model, User user)
        {
            if (model.EmailOfReceiver is null)
            {
                throw new ArgumentNullException("Email cannot be null");
            }
            var registratedUser = _userRepository.IsTherUserWithThisEmail(model.EmailOfReceiver);
            if (registratedUser)
            {
                throw new ArgumentException("There is registrated user with this email");
            }
            var now = DateTime.Now;
            var invitation = new Invitation()
            {
                Id = Guid.NewGuid(),
                CreationOfInvite = now,
                UserCreatedTheInvite = user.Id,
                ExpirationOfInvite = now.AddMinutes(10),
                UserRegistratedFromInvite = model.EmailOfReceiver,
                VerificationCode = CreateRandomVerificationCode()
            };
            await _invitationRepository.AddInvitationAsync(invitation);
            _emailSender.SendEmail(model.EmailOfReceiver, invitation.VerificationCode);
        }

        public string CreateRandomVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000);
            return code.ToString("D6");
        }
    }
}
