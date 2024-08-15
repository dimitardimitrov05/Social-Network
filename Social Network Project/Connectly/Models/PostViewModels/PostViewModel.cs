using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.PostViewModels
{
    public class PostViewModel
    {
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        public string UserFirstName { get; set; } = null!;
        [Required]
        public string UserLastName { get; set; } = null!;
        [Required]
        public DateTime CreationOfPost { get; set; }
        [Required]
        public string Visibility { get; set; } = null!;
    }
}
