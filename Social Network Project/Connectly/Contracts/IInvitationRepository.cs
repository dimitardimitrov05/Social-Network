using Connectly.Data.Entities;

namespace Connectly.Contracts
{
    public interface IInvitationRepository
    {
        Task AddInvitationAsync(Invitation invitation);
        Task<Invitation> FindInvitationByEmailAsync(string email);
    }
}
