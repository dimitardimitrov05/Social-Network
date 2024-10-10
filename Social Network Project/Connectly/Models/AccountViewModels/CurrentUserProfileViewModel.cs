using Connectly.Models.PostViewModels;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.AccountViewModels
{
    public class CurrentUserProfileViewModel
    {
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

        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}
