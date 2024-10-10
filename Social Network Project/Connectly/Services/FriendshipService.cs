using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Data.Entities;
using Connectly.Models.FriendshipViewModels;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly ApplicationDbContext _context;

        public FriendshipService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AcceptFriendRequestAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == currentUserId && x.UserThatAcceptedOrDeclinedTheFriendship == otherUserId) ||
                            (x.UserThatSendTheFriendship == otherUserId && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId) &&
                            (x.StatusOfFriendship == "Waiting"))
                .FirstOrDefaultAsync();

            if (friendship == null)
            {
                throw new ArgumentNullException("There isn't such friendship");
            }
            friendship.StatusOfFriendship = "Accepted";
            await _context.SaveChangesAsync();
        }

        public async Task CreateFriendshipAsync(CreateFriendshipFromAcceptedInvitationViewModel model)
        {
            var friendship = new Friendship()
            {
                Id = Guid.NewGuid(),
                DateOfSendingFriendship = DateTime.Now,
                UserThatSendTheFriendship = model.UserSendedFriendship,
                DateOfAcceptingOrDecliningTheFriendship = DateTime.Now,
                UserThatAcceptedOrDeclinedTheFriendship = model.UserAcceptedFriendship,
                RemovingFriendship = null,
                UserThatRemovedTheFriendship = null,
                StatusOfFriendship = "Accepted"
            };
            await _context.Friendships.AddAsync(friendship);

            var sender = await _context.Users.Include(x => x.UserFriendships).FirstOrDefaultAsync(x => x.Id == model.UserSendedFriendship);
            var receiver = await _context.Users.Include(x => x.UserFriendships).FirstOrDefaultAsync(x => x.Id == model.UserAcceptedFriendship);

            if (sender == null || receiver == null)
            {
                throw new ArgumentException("Something went wrong");
            }

            var userFriendship1 = new UserFriendship()
            {
                UserId = sender.Id,
                User = sender,
                FriendshipId = friendship.Id,
                Friendship = friendship
            };

            var userFriendship2 = new UserFriendship()
            {
                UserId = receiver.Id,
                User = receiver,
                FriendshipId = friendship.Id,
                Friendship = friendship
            };

            sender.UserFriendships.Add(userFriendship1);
            receiver.UserFriendships.Add(userFriendship2);
            await _context.SaveChangesAsync();
        }

        public async Task DeclineFriendRequestAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == currentUserId && x.UserThatAcceptedOrDeclinedTheFriendship == otherUserId) ||
                            (x.UserThatSendTheFriendship == otherUserId && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId) &&
                            (x.StatusOfFriendship == "Waiting"))
                .FirstOrDefaultAsync();

            if (friendship == null)
            {
                throw new ArgumentNullException("There isn't such friendship");
            }
            friendship.StatusOfFriendship = "Declined";
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFriendshipAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == currentUserId && x.UserThatAcceptedOrDeclinedTheFriendship == otherUserId) ||
                            (x.UserThatSendTheFriendship == otherUserId && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId))
                .FirstOrDefaultAsync();

            if (friendship == null)
            {
                throw new ArgumentNullException("There isn't such friendship");
            }
            friendship.StatusOfFriendship = "Removed";
            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendsViewModel>> FriendsOfUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException("There isn't such user");
            }

            var friends = await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == userId) &&
                            (x.StatusOfFriendship == "Accepted"))
                .Select(x => x.UserThatAcceptedOrDeclinedTheFriendship)
                .ToListAsync();

            var acceptedFriends = await _context.Friendships
                .Where(x => (x.UserThatAcceptedOrDeclinedTheFriendship == userId) &&
                            (x.StatusOfFriendship == "Accepted"))
                .Select(x => x.UserThatSendTheFriendship)
                .ToListAsync();

            var result = await _context.Users
                .Where(x => friends.Any(f => x.Id == f) ||
                       acceptedFriends.Any(f => x.Id == f))
                .ToListAsync();

            return result.Select(x => new FriendsViewModel()
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
            })
                .ToList();
        }

        public async Task<string> IsFriendAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _context.Friendships
                .Where(x => (x.UserThatSendTheFriendship == currentUserId && x.UserThatAcceptedOrDeclinedTheFriendship == otherUserId ) || 
                            (x.UserThatSendTheFriendship == otherUserId && x.UserThatAcceptedOrDeclinedTheFriendship == currentUserId))
                .FirstOrDefaultAsync();

            if (friendship == null || friendship.StatusOfFriendship == "Removed" || friendship.StatusOfFriendship == "Declined")
            {
                return "No";
            }
            else if (friendship.StatusOfFriendship == "Accepted")
            {
                return "Yes";
            }
            else if (friendship.StatusOfFriendship == "Waiting" && friendship.UserThatSendTheFriendship == currentUserId)
            {
                return "Sent";
            }
            else if (friendship.StatusOfFriendship == "Waiting" && friendship.UserThatSendTheFriendship == otherUserId)
            {
                return "Received";
            }
            else
            {
                throw new ArgumentException("Something went wrong");
            }
        }

        public async Task<List<FriendRequestsViewModel>> ListFriendRequestsAsync(string userId)
        {
            var userIds = await _context.Friendships
                .Where(x => x.StatusOfFriendship == "Waiting" && x.UserThatAcceptedOrDeclinedTheFriendship == userId)
                .Select(x => x.UserThatSendTheFriendship)
                .ToListAsync();

            var list = new List<FriendRequestsViewModel>();

            foreach (var id in userIds) 
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    throw new ArgumentException("Something went wrong!");
                }
                var model = new FriendRequestsViewModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfilePicture = user.Image
                };
                list.Add(model);
            }
            return list;
        }

        public async Task<List<SentRequestsViewModel>> ListSentFriendRequestsAsync(string currentUserId)
        {
            var userIds = await _context.Friendships
                .Where(x => x.StatusOfFriendship == "Waiting" && x.UserThatSendTheFriendship == currentUserId)
                .Select(x => x.UserThatAcceptedOrDeclinedTheFriendship)
                .ToListAsync();

            var list = new List<SentRequestsViewModel>();

            foreach (var id in userIds)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    throw new ArgumentException("Something went wrong!");
                }
                var model = new SentRequestsViewModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfilePicture = user.Image
                };
                list.Add(model);
            }
            return list;
        }

        public async Task SendFriendshipAsync(SendFriendshipViewModel model)
        {
            var friendship = new Friendship()
            {
                Id = Guid.NewGuid(),
                DateOfSendingFriendship = DateTime.Now,
                UserThatSendTheFriendship = model.SenderId,
                DateOfAcceptingOrDecliningTheFriendship = default(DateTime),
                UserThatAcceptedOrDeclinedTheFriendship = model.ReceiverId,
                RemovingFriendship = null,
                UserThatRemovedTheFriendship = null,
                StatusOfFriendship = "Waiting"
            };

            await _context.Friendships.AddAsync(friendship);
            await _context.SaveChangesAsync();
        }
    }
}
