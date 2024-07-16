using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Entities
{
    public class Friendship
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public DateTime SendingFriendship { get; set; }
        [Required]
        public Guid UserThatSendTheFriendship { get; set; }
        public DateTime DateOfAcceptingOrDecliningTheFriendship { get; set; }
        public Guid UserThatAcceptedOrDeclinedTheFriendship { get; set; }
        public DateTime RemovingFriendship { get; set; }
        public Guid UserThatRemovedTheFriendship { get; set; }
        [Required]
        public string StatusOfFriendship { get; set; } = null!;
    }
}