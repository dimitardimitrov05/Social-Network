using Connectly.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.AccountViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string AccountPrivacy { get; set; } = null!;
        public string? Image { get; set; }
        [Required]
        public string IsFriendWithCurrentUser { get; set; } = null!;
    }
}
