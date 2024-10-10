using Connectly.Data.Entities;

namespace Connectly.Contracts
{
    public interface IPostRepository
    {
        Task<Post> FindPostById(Guid postId);
        Task<Post> AddPost(Post post);
        Task<List<Post>> GetCurrentUserPosts(string currentUserId);
        Task<List<Post>> GetAllVisiblePostsForCurrentUser(string currentUserId);
        Task<List<Post>> GetOneUserVisiblePostsForCurrentUser(string currentUserId, string otherUserId);
        Task DeletePost(Post post);
    }
}
