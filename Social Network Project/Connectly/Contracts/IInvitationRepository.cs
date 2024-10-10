using Connectly.Data.Entities;

namespace Connectly.Contracts
{
    public interface IInvitationRepository
    {
        Task AddInvitation(Invitation invitation);
        Task<Invitation> FindInvitationByEmail(string email);
    }
}
