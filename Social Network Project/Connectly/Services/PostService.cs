using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Data.Entities;
using Connectly.Models.PostViewModels;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePostAsync(IndexViewModel model, User user)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                CreationOfPost = DateTime.Now,
                UserId = user.Id,
                Text = model.PostContent!,
                Visibility = model.PostVisibility!
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PostViewModel>> CurrentUserPostsAsync(string currentUserId)
        {
            return await _context.Posts.Where(x => x.UserId == currentUserId)
                .Select(x => new PostViewModel()
                {
                    Id = x.Id,
                    Text = x.Text,
                    UserFirstName = x.User.FirstName,
                    UserLastName = x.User.LastName,
                    UserProfilePicture = x.User.Image,
                    CreationOfPost = x.CreationOfPost,
                    Visibility = x.Visibility,
                    UserId = x.UserId,
                }).ToListAsync();
        }

        public async Task DeletePostAsync(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                throw new ArgumentNullException("There isn't post with this id");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PostViewModel>> ListPostsAsync(string currentUserId)
        {
            var userFriendIds = await _context.Friendships
           .Where(f => f.StatusOfFriendship == "Accepted" &&
                      (f.UserThatSendTheFriendship == currentUserId || f.UserThatAcceptedOrDeclinedTheFriendship == currentUserId))
           .Select(f => f.UserThatSendTheFriendship == currentUserId ? f.UserThatAcceptedOrDeclinedTheFriendship : f.UserThatSendTheFriendship)
           .ToListAsync();

            userFriendIds.Add(currentUserId);

            var friendsOfFriendsIds = await _context.Friendships
                .Where(f => f.StatusOfFriendship == "Accepted" &&
                           (userFriendIds.Contains(f.UserThatSendTheFriendship) || userFriendIds.Contains(f.UserThatAcceptedOrDeclinedTheFriendship)))
                .Select(f => f.UserThatSendTheFriendship != currentUserId && !userFriendIds.Contains(f.UserThatSendTheFriendship) ? f.UserThatSendTheFriendship : f.UserThatAcceptedOrDeclinedTheFriendship)
                .Distinct()
                .ToListAsync();

            var visiblePosts = await _context.Posts
                .Include(p => p.User)
                .ThenInclude(x => x.UserFriendships)
                .ThenInclude(x => x.Friendship)
                .Where(p =>
                    p.Visibility == "Public" ||
                    (p.Visibility == "Friends" && userFriendIds.Contains(p.UserId))  ||
                    (p.Visibility == "Friends Of friends" && (userFriendIds.Contains(p.UserId) || friendsOfFriendsIds.Contains(p.UserId))))
                .ToListAsync();

            return visiblePosts
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Text = x.Text,
                    UserFirstName = x.User.FirstName,
                    UserLastName = x.User.LastName,
                    UserProfilePicture = x.User.Image,
                    CreationOfPost = x.CreationOfPost,
                    Visibility = x.Visibility,
                    UserId = x.UserId,
                }).OrderByDescending(x => x.CreationOfPost)
                .ToList();
        }

        public async Task<List<PostViewModel>> UserPostsAsync(string currentUserId, string otherUserId)
        {
            var userFriendIds = await _context.Friendships
           .Where(f => f.StatusOfFriendship == "Accepted" &&
                      (f.UserThatSendTheFriendship == currentUserId || f.UserThatAcceptedOrDeclinedTheFriendship == currentUserId))
           .Select(f => f.UserThatSendTheFriendship == currentUserId ? f.UserThatAcceptedOrDeclinedTheFriendship : f.UserThatSendTheFriendship)
           .ToListAsync();

            userFriendIds.Add(currentUserId);

            var friendsOfFriendsIds = await _context.Friendships
                .Where(f => f.StatusOfFriendship == "Accepted" &&
                           (userFriendIds.Contains(f.UserThatSendTheFriendship) || userFriendIds.Contains(f.UserThatAcceptedOrDeclinedTheFriendship)))
                .Select(f => f.UserThatSendTheFriendship != currentUserId && !userFriendIds.Contains(f.UserThatSendTheFriendship) ? f.UserThatSendTheFriendship : f.UserThatAcceptedOrDeclinedTheFriendship)
                .Distinct()
                .ToListAsync();

            var visiblePosts = await _context.Posts
                .Include(p => p.User)
                .ThenInclude(x => x.UserFriendships)
                .ThenInclude(x => x.Friendship)
                .Where(p => p.UserId == otherUserId &&
                        (p.Visibility == "Public" ||
                        (p.Visibility == "Friends" && userFriendIds.Contains(otherUserId)) ||
                        (p.Visibility == "Friends of friends" && (userFriendIds.Contains(otherUserId) || friendsOfFriendsIds.Contains(otherUserId)))))
            .OrderByDescending(p => p.CreationOfPost)
            .ToListAsync();

            return visiblePosts
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Text = x.Text,
                    UserFirstName = x.User.FirstName,
                    UserLastName = x.User.LastName,
                    UserProfilePicture = x.User.Image,
                    CreationOfPost = x.CreationOfPost,
                    Visibility = x.Visibility,
                    UserId = x.UserId,
                }).OrderByDescending(x => x.CreationOfPost)
                .ToList();
        }
    }
}
