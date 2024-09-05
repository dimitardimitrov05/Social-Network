using Connectly.Data.Account;
using Connectly.Models.InvitationViewModels;
using Connectly.Models.PostViewModels;

namespace Connectly.Contracts
{
    public interface IInvitationService
    {
        Task CreateIvitationAsync(IndexViewModel model, User user);
        string CreateRandomVerificationCode();
    }
}
