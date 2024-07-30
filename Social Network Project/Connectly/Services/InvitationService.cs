using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Data.Entities;
using Connectly.Models.InvitationViewModels;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public InvitationService(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task CreateIvitationAsync(CreateInviteViewModel model, User user)
        {
            var registratedUser = await _context.Users.Where(x => x.Email == model.EmailOfReceiver).FirstOrDefaultAsync();
            if (registratedUser != null)
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
            await _context.Invitations.AddAsync(invitation);
            await _context.SaveChangesAsync();
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
