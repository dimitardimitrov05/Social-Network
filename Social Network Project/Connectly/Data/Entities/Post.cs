using Connectly.Data.Account;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Entities
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationOfPost { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        public User User { get; set; }
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        public string Visibility { get; set; } = null!;
    }
}
