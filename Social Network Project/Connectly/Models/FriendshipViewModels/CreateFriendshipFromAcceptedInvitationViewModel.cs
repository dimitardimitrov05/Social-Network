using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.FriendshipViewModels
{
    public class CreateFriendshipFromAcceptedInvitationViewModel
    {
        [Required]
        public string UserAcceptedFriendship { get; set; } = null!;
        [Required]
        public string UserSendedFriendship { get; set; } = null!;
    }
}
