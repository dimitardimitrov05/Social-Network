using Connectly.Data.Account;
using Connectly.Models.InvitationViewModels;

namespace Connectly.Contracts
{
    public interface IInvitationService
    {
        Task CreateIvitationAsync(CreateInviteViewModel model, User user);
        string CreateRandomVerificationCode();
    }
}
