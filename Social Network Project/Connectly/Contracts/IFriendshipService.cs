using Connectly.Models.FriendshipViewModels;

namespace Connectly.Contracts
{
    public interface IFriendshipService
    {
        Task CreateFriendshipAsync(CreateFriendshipFromAcceptedInvitationViewModel model);
        Task SendFriendshipAsync(SendFriendshipViewModel model);
        Task<string> IsFriendAsync(string currentUserId, string otherUserId);
        Task AcceptFriendRequestAsync(string  currentUserId, string otherUserId);
        Task DeclineFriendRequestAsync(string currentUserId, string otherUserId);
        Task DeleteFriendshipAsync(string  currentUserId, string otherUserId);
        Task<List<FriendsViewModel>> FriendsOfUserAsync(string userId);
    }
}
