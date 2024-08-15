using Connectly.Data.Account;
using Connectly.Models.PostViewModels;

namespace Connectly.Contracts
{
    public interface IPostService
    {
        Task<List<PostViewModel>> ListPosts();
        Task CreatePostAsync(IndexViewModel model, User user);
    }
}
