using Connectly.Data.Account;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Entities
{
    public class Friendship
    {
        [Key] 
        public Guid Id { get; set; }
        [Required]
        public DateTime DateOfSendingFriendship { get; set; }
        [Required]
        public string UserThatSendTheFriendship { get; set; } = null!;
        public DateTime DateOfAcceptingOrDecliningTheFriendship { get; set; }
        public string? UserThatAcceptedOrDeclinedTheFriendship { get; set; }
        public DateTime? RemovingFriendship { get; set; }
        public string? UserThatRemovedTheFriendship { get; set; }
        [Required]
        public string StatusOfFriendship { get; set; } = null!;
        public ICollection<UserFriendship> UserFriendships { get; set; } = new List<UserFriendship>();
    }
}