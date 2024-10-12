using Connectly.Data.Entities;

namespace Connectly.Contracts
{
    public interface IFriendshipRepository
    {
        Task<Friendship> FindExistingDeclinedOrRemovedFriendship(string senderId, string receiverId);
        Task<Friendship> FindFriendshipByTwoIdsAsync(string currentUserId, string otherUserId);
        Task<Friendship> FindFriendshipWithWaitingStatusByTwoIdsAsync(string currentUserId, string otherUserId);
        Task<Friendship> FindFriendshipWithAcceptedStatusByTwoIdsAsync(string currentUserId, string otherUserId);
        Task<List<string>> FindIdsOfCurrnetUserAcceptedFriendsAsync(string currentUserId);
        Task<List<string>> FindIdsOfCurrnetUserFriendsThatHeAcceptedAsync(string currentUserId);
        Task<List<string>> FindIdsOfAllFriendsAsync(string currentUserId);
        Task<List<string>> FindIdsOfAllFriendsOfFriendsAsync(string currentUserId);
        Task<List<string>> FindIdsOfUsersThatSentFriendRequestToCurrentUserAsync(string currentUserId);
        Task<List<string>> FindIdsOfUsersThatCurrentUserSentFriendRequestToAsync(string currentUserId);
        Task AddFriendshipAsync(Friendship friendship);
        Task EditFriendshipAsync(Friendship friendship);
        bool AreThereFriendRequests(string currentUserId);
    }
}
