using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Entities
{
    public class Invitation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationOfInvite { get; set; }
        [Required]
        public Guid UserCreatedTheInvite { get; set; }
        [Required]
        public DateTime ExpirationOfInvite { get; set; }
        [Required]
        public string VerificationCode { get; set; } = null!;
    }
}
