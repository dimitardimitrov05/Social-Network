using Connectly.Models.FriendshipViewModels;

namespace Connectly.Contracts
{
    public interface IFriendshipService
    {
        Task CreateFriendshipAsync(CreateFriendshipFromAcceptedInvitationViewModel model);
    }
}
