using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreationOfPost { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        public string Visibility { get; set; } = null!;
    }
}
