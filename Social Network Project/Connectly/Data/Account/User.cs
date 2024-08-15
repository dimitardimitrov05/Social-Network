using Connectly.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Data.Account
{
    public class User : IdentityUser
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
        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
        public ICollection<UserFriendship> UserFriendships { get; set; } = new List<UserFriendship>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
