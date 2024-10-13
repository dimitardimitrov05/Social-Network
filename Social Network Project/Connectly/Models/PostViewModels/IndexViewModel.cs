using Connectly.Models.Pagination;
using System.ComponentModel.DataAnnotations;

namespace Connectly.Models.PostViewModels
{
    public class IndexViewModel
    {
        [Required]
        public string CurrentUserId { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        [EmailAddress]
        public string? EmailOfReceiver { get; set; }
        public string? PostContent { get; set; }
        public string? PostVisibility { get; set; }
        public PaginatedList<PostViewModel>? Posts { get; set; }
    }
}
