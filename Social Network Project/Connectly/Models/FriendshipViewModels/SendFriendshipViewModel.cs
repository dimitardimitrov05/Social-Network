using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.FriendshipViewModels
{
    public class SendFriendshipViewModel
    {
        [Required]
        public string SenderId { get; set; } = null!;
        [Required]
        public string ReceiverId { get; set; } = null!;
    }
}
