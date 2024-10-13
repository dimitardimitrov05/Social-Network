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
        private readonly IPostRepository _postRepository;
        private readonly IFriendshipRepository _friendshipRepository;

        public PostService(IPostRepository postRepository, IFriendshipRepository friendshipRepository)
        {
            _postRepository = postRepository;
            _friendshipRepository = friendshipRepository;
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
            await _postRepository.AddPostAsync(post);
        }

        public async Task<List<PostViewModel>> CurrentUserPostsAsync(string currentUserId)
        {
            var post = await _postRepository.GetCurrentUserPostsAsync(currentUserId);
            return post.Select(x => new PostViewModel()
            {
                Id = x.Id,
                Text = x.Text,
                UserFirstName = x.User.FirstName,
                UserLastName = x.User.LastName,
                UserProfilePicture = x.User.Image,
                CreationOfPost = x.CreationOfPost,
                Visibility = x.Visibility,
                UserId = x.UserId,
            }).ToList();
        }

        public async Task DeletePostAsync(Guid id)
        {
            var post = await _postRepository.FindPostByIdAsync(id);
            if (post == null)
            {
                throw new ArgumentNullException("There isn't post with this id");
            }

            await _postRepository.DeletePostAsync(post);
        }

        public async Task<IQueryable<PostViewModel>> ListPostsAsync(string currentUserId)
        {
            var userFriendIds = await _friendshipRepository.FindIdsOfAllFriendsAsync(currentUserId);

            var friendsOfFriendsIds = await _friendshipRepository.FindIdsOfAllFriendsOfFriendsAsync(currentUserId);

            var visiblePosts = await _postRepository.GetAllVisiblePostsForCurrentUserAsync(currentUserId);

            return visiblePosts;
        }

        public async Task<List<PostViewModel>> UserPostsAsync(string currentUserId, string otherUserId)
        {
            var userFriendIds = await _friendshipRepository.FindIdsOfAllFriendsAsync(currentUserId);

            var friendsOfFriendsIds = await _friendshipRepository.FindIdsOfAllFriendsOfFriendsAsync(currentUserId);

            var visiblePosts = await _postRepository.GetOneUserVisiblePostsForCurrentUserAsync(currentUserId, otherUserId);

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
