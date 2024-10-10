namespace Connectly.Models.FriendshipViewModels
{
    public class IndexFriendsViewModel
    {
        public string? EmailOfReceiver { get; set; }
        public List<FriendsViewModel> Friends { get; set; } = new List<FriendsViewModel>();
    }
}
