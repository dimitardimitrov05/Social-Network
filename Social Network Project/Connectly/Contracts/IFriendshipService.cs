using Connectly.Models.FriendshipViewModels;

namespace Connectly.Contracts
{
    public interface IFriendshipService
    {
        Task CreateFriendshipAsync(CreateFriendshipFromAcceptedInvitationViewModel model);
        Task SendFriendshipAsync(SendFriendshipViewModel model);
        Task<string> IsFriendAsync(string currentUserId, string otherUserId);
        Task AcceptFriendRequest(string  currentUserId, string otherUserId);
        Task DeclineFriendRequest(string currentUserId, string otherUserId);
    }
}
