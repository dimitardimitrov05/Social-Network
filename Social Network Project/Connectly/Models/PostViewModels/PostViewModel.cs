using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.PostViewModels
{
    public class PostViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        public string UserFirstName { get; set; } = null!;
        [Required]
        public string UserLastName { get; set; } = null!;
        public string? UserProfilePicture { get; set; }
        [Required]
        public DateTime CreationOfPost { get; set; }
        [Required]
        public string Visibility { get; set; } = null!;
        [Required]
        public string UserId { get; set; } = null!;
    }
}
