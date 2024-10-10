using Connectly.Data.Entities;

namespace Connectly.Contracts
{
    public interface IFriendshipRepository
    {
        Task<Friendship> FindFriendshipWithWaitingStatusByTwoIds(string currentUserId, string otherUserId);
        Task<Friendship> FindFriendshipWithAcceptedStatusByTwoIds(string currentUserId, string otherUserId);
        Task<List<string>> FindIdsOfCurrnetUserAcceptedFriends(string currentUserId);
        Task<List<string>> FindIdsOfCurrnetUserFriendsThatHeAccepted(string currentUserId);
        Task<List<string>> FindIdsOfAllFriends(string currentUserId);
        Task<List<string>> FindIdsOfAllFriendsOfFriends(string currentUserId);
        Task<List<string>> FindIdsOfUsersThatSentFriendRequestToCurrentUser(string currentUserId);
        Task<List<string>> FindIdsOfUsersThatCurrentUserSentFriendRequestTo(string currentUserId);
        Task AddFriendship(Friendship friendship);
        Task EditFriendship(Friendship friendship);
        Task<bool> AreThereFriendRequests(string currentUserId);
    }
}
