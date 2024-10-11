using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFriendshipRepository _friendshipRepository;

        public UserRepository(ApplicationDbContext context, IFriendshipRepository friendshipRepository)
        {
            _context = context;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<List<User>> FindCurrentUserFriendsAsync(string currentUserId)
        {
            var friends = await _friendshipRepository.FindIdsOfCurrnetUserAcceptedFriendsAsync(currentUserId);

            var acceptedFriends = await _friendshipRepository.FindIdsOfCurrnetUserFriendsThatHeAcceptedAsync(currentUserId);
 
            return await _context.Users
                .Where(x => friends.Any(f => x.Id == f) ||
                       acceptedFriends.Any(f => x.Id == f))
                .ToListAsync();
        }

        public bool IsTherUserWithThisEmail(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            return await _context.Users.Include(x => x.UserFriendships).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
