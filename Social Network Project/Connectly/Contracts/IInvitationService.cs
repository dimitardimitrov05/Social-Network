using Connectly.Data.Account;
using Connectly.Data.Entities;
using Connectly.Models.InvitationViewModels;
using Connectly.Models.PostViewModels;

namespace Connectly.Contracts
{
    public interface IInvitationService
    {
        Task<Invitation> FindInvitationByEmailAsync(string email);
        Task CreateIvitationAsync(IndexViewModel model, User user);
        string CreateRandomVerificationCode();
    }
}
