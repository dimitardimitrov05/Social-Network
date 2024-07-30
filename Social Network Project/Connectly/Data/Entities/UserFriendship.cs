using Connectly.Data.Account;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Entities
{
    public class UserFriendship
    {
        [Required]
        public string UserId { get; set; } = null!;
        public User User { get; set; }
        [Required]
        public Guid FriendshipId { get; set; }
        public Friendship Friendship { get; set; }
    }
}
