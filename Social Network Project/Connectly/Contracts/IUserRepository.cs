using Connectly.Data.Account;

namespace Connectly.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindUserById(string id);
        Task<User> FindUserByEmail(string email);
        Task<List<User>> FindCurrentUserFriends(string currentUserId);
    }
}
