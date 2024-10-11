using Connectly.Data.Entities;

namespace Connectly.Contracts
{
    public interface IPostRepository
    {
        Task<Post> FindPostByIdAsync(Guid postId);
        Task<List<Post>> GetCurrentUserPostsAsync(string currentUserId);
        Task<List<Post>> GetAllVisiblePostsForCurrentUserAsync(string currentUserId);
        Task<List<Post>> GetOneUserVisiblePostsForCurrentUserAsync(string currentUserId, string otherUserId);
        Task AddPostAsync(Post post);
        Task DeletePostAsync(Post post);
    }
}
