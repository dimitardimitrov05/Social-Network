using Connectly.Data.Account;
using Connectly.Models.PostViewModels;

namespace Connectly.Contracts
{
    public interface IPostService
    {
        Task<List<PostViewModel>> ListPosts(string cuurentUserId);
        Task CreatePostAsync(IndexViewModel model, User user);
        Task DeletePostAsync(Guid id);
    }
}
