using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.InvitationViewModels
{
    public class CreateInviteViewModel
    {
        [Required]
        public string EmailOfReceiver { get; set; } = null!;
    }
}
