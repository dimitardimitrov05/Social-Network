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
        public string LastName { get; private set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string AccountPrivacy { get; set; } = null!;
        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
        public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
