using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly ApplicationDbContext _context;

        public FriendshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFriendshipAsync(Friendship friendship)
        {
            await _context.Friendships.AddAsync(friendship);
            await _context.SaveChangesAsync();
        }

        public bool AreThereFriendRequests(string currentUserId)
        {
            return _context.Friendships.Any(x => x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId && x.StatusOfFriendship == "Waiting");
        }

        public async Task EditFriendshipAsync(Friendship friendship)
        {
            _context.Friendships.Update(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task<Friendship> FindFriendshipByTwoIdsAsync(string currentUserId, string otherUserId)
        {
            return await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == currentUserId && x.UserThatAcceptedOrDeclinedTheFriendship == otherUserId) ||
                            (x.UserThatSendTheFriendship == otherUserId && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId))
                .FirstOrDefaultAsync();
        }

        public async Task<Friendship> FindFriendshipWithAcceptedStatusByTwoIdsAsync(string currentUserId, string otherUserId)
        {
            return await _context.Friendships
               .Where(x => (x.UserThatSendTheFriendship == currentUserId && x.UserThatAcceptedOrDeclinedTheFriendship == otherUserId) ||
                           (x.UserThatSendTheFriendship == otherUserId && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId) &&
                           (x.StatusOfFriendship == "Accepted"))
               .FirstAsync();
        }

        public async Task<Friendship> FindFriendshipWithWaitingStatusByTwoIdsAsync(string currentUserId, string otherUserId)
        {
            return await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == currentUserId && x.UserThatAcceptedOrDeclinedTheFriendship == otherUserId) ||
                            (x.UserThatSendTheFriendship == otherUserId && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId) &&
                            (x.StatusOfFriendship == "Waiting"))
                .FirstAsync();
        }

        public async Task<List<string>> FindIdsOfAllFriendsAsync(string currentUserId)
        {
            return await _context.Friendships
           .Where(f => f.StatusOfFriendship == "Accepted" &&
                      (f.UserThatSendTheFriendship == currentUserId || f.UserThatAcceptedOrDeclinedTheFriendship == currentUserId))
           .Select(f => f.UserThatSendTheFriendship == currentUserId ? f.UserThatAcceptedOrDeclinedTheFriendship : f.UserThatSendTheFriendship)
           .ToListAsync();
        }

        public async Task<List<string>> FindIdsOfAllFriendsOfFriendsAsync(string currentUserId)
        {
            var userFriendIds = await FindIdsOfAllFriendsAsync(currentUserId);

            userFriendIds.Add(currentUserId);

            return await _context.Friendships
                .Where(f => f.StatusOfFriendship == "Accepted" &&
                           (userFriendIds.Contains(f.UserThatSendTheFriendship) || userFriendIds.Contains(f.UserThatAcceptedOrDeclinedTheFriendship)))
                .Select(f => f.UserThatSendTheFriendship != currentUserId && !userFriendIds.Contains(f.UserThatSendTheFriendship) ? f.UserThatSendTheFriendship : f.UserThatAcceptedOrDeclinedTheFriendship)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<string>> FindIdsOfCurrnetUserAcceptedFriendsAsync(string currentUserId)
        {
            return await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == currentUserId) &&
                            (x.StatusOfFriendship == "Accepted"))
                .Select(x => x.UserThatAcceptedOrDeclinedTheFriendship)
                .ToListAsync();
        }

        public async Task<List<string>> FindIdsOfCurrnetUserFriendsThatHeAcceptedAsync(string currentUserId)
        {
            return await _context.Friendships
                .Where(x => (x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId) &&
                            (x.StatusOfFriendship == "Accepted"))
                .Select(x => x.UserThatSendTheFriendship)
                .ToListAsync();
        }

        public async Task<List<string>> FindIdsOfUsersThatCurrentUserSentFriendRequestToAsync(string currentUserId)
        {
            return await _context.Friendships
                .Where(x => x.StatusOfFriendship == "Waiting" && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId)
                .Select(x => x.UserThatSendTheFriendship)
                .ToListAsync();
        }

        public async Task<List<string>> FindIdsOfUsersThatSentFriendRequestToCurrentUserAsync(string currentUserId)
        {
            return await _context.Friendships
                .Where(x => x.StatusOfFriendship == "Waiting" && x.UserThatSendTheFriendship == currentUserId)
                .Select(x => x.UserThatAcceptedOrDeclinedTheFriendship)
                .ToListAsync();
        }
    }
}
