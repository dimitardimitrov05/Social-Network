using Connectly.Data.Account;

namespace Connectly.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindUserByIdAsync(string id);
        bool IsTherUserWithThisEmail(string email);
        Task<List<User>> FindCurrentUserFriendsAsync(string currentUserId);
    }
}
