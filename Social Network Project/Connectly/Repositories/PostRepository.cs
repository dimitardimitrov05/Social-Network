using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFriendshipRepository _friendshipRepository;

        public PostRepository(ApplicationDbContext context, IFriendshipRepository friendshipRepository)
        {
            _context = context;
            _friendshipRepository = friendshipRepository;
        }

        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> FindPostByIdAsync(Guid postId)
        {
            return await _context.Posts.FirstAsync(x => x.Id == postId);
        }

        public async Task<List<Post>> GetAllVisiblePostsForCurrentUserAsync(string currentUserId)
        {
            var userFriendIds = await _friendshipRepository.FindIdsOfAllFriendsAsync(currentUserId);

            var friendsOfFriendsIds = await _friendshipRepository.FindIdsOfAllFriendsOfFriendsAsync(currentUserId);

            return await _context.Posts
                .Include(p => p.User)
                .ThenInclude(x => x.UserFriendships)
                .ThenInclude(x => x.Friendship)
                .Where(p =>
                    p.Visibility == "Public" ||
                    (p.UserId == currentUserId) ||
                    (p.Visibility == "Friends" && userFriendIds.Contains(p.UserId)) ||
                    (p.Visibility == "Friends Of friends" && (userFriendIds.Contains(p.UserId) || friendsOfFriendsIds.Contains(p.UserId))))
                .ToListAsync();
        }

        public async Task<List<Post>> GetCurrentUserPostsAsync(string currentUserId)
        {
            return await _context.Posts.Where(x => x.UserId == currentUserId).ToListAsync();
        }

        public async Task<List<Post>> GetOneUserVisiblePostsForCurrentUserAsync(string currentUserId, string otherUserId)
        {
            var userFriendIds = await _friendshipRepository.FindIdsOfAllFriendsAsync(currentUserId);

            var friendsOfFriendsIds = await _friendshipRepository.FindIdsOfAllFriendsOfFriendsAsync(currentUserId);

            return await _context.Posts
                .Include(p => p.User)
                .ThenInclude(x => x.UserFriendships)
                .ThenInclude(x => x.Friendship)
                .Where(p => p.UserId == otherUserId &&
                        (p.Visibility == "Public" ||
                        (p.Visibility == "Friends" && userFriendIds.Contains(otherUserId)) ||
                        (p.Visibility == "Friends of friends" && (userFriendIds.Contains(otherUserId) || friendsOfFriendsIds.Contains(otherUserId)))))
            .OrderByDescending(p => p.CreationOfPost)
            .ToListAsync();
        }
    }
}
