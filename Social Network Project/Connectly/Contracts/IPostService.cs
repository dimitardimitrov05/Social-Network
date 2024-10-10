using Connectly.Data.Account;
using Connectly.Models.PostViewModels;

namespace Connectly.Contracts
{
    public interface IPostService
    {
        Task<List<PostViewModel>> ListPostsAsync(string currentUserId);
        Task CreatePostAsync(IndexViewModel model, User user);
        Task DeletePostAsync(Guid id);
        Task<List<PostViewModel>> CurrentUserPostsAsync(string cuurentUserId);
        Task<List<PostViewModel>> UserPostsAsync(string currentUserId, string otherUserId);
    }
}
