using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.FriendshipViewModels
{
    public class FriendsViewModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
    }
}
