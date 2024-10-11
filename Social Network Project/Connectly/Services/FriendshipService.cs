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
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUserRepository _userRepository;

        public FriendshipService(IFriendshipRepository friendshipRepository, IUserRepository userRepository)
        {
            _friendshipRepository = friendshipRepository;
            _userRepository = userRepository;
        }

        public async Task AcceptFriendRequestAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _friendshipRepository.FindFriendshipWithWaitingStatusByTwoIdsAsync(currentUserId, otherUserId);

            if (friendship == null)
            {
                throw new ArgumentNullException("There isn't such friendship");
            }
            friendship.StatusOfFriendship = "Accepted";
            await _friendshipRepository.EditFriendshipAsync(friendship);
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

            var sender = await _userRepository.FindUserByIdAsync(model.UserSendedFriendship);
            var receiver = await _userRepository.FindUserByIdAsync(model.UserAcceptedFriendship);

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
            await _friendshipRepository.AddFriendshipAsync(friendship);
        }

        public async Task DeclineFriendRequestAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _friendshipRepository.FindFriendshipWithWaitingStatusByTwoIdsAsync(currentUserId, otherUserId);
            if (friendship == null)
            {
                throw new ArgumentNullException("There isn't such friendship");
            }
            friendship.StatusOfFriendship = "Declined";
            await _friendshipRepository.EditFriendshipAsync(friendship);
        }

        public async Task DeleteFriendshipAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _friendshipRepository.FindFriendshipWithAcceptedStatusByTwoIdsAsync(currentUserId, otherUserId);

            if (friendship == null)
            {
                throw new ArgumentNullException("There isn't such friendship");
            }
            friendship.StatusOfFriendship = "Removed";
            friendship.RemovingFriendship = DateTime.Now;
            friendship.UserThatRemovedTheFriendship = currentUserId;
            await _friendshipRepository.EditFriendshipAsync(friendship);
        }

        public async Task<List<FriendsViewModel>> FriendsOfUserAsync(string userId)
        {
            var user = await _userRepository.FindUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException("There isn't such user");
            }

            var friends = await _friendshipRepository.FindIdsOfCurrnetUserAcceptedFriendsAsync(userId);

            var acceptedFriends = await _friendshipRepository.FindIdsOfCurrnetUserFriendsThatHeAcceptedAsync(userId);

            var result = await _userRepository.FindCurrentUserFriendsAsync(userId);

            return result.Select(x => new FriendsViewModel()
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }).ToList();
        }

        public async Task<string> IsFriendAsync(string currentUserId, string otherUserId)
        {
            var friendship = await _friendshipRepository.FindFriendshipByTwoIdsAsync(currentUserId, otherUserId);

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
            var userIds = await _friendshipRepository.FindIdsOfUsersThatCurrentUserSentFriendRequestToAsync(userId);

            var list = new List<FriendRequestsViewModel>();

            foreach (var id in userIds) 
            {
                var user = await _userRepository.FindUserByIdAsync(id);
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
            var userIds = await _friendshipRepository.FindIdsOfUsersThatSentFriendRequestToCurrentUserAsync(currentUserId);

            var list = new List<SentRequestsViewModel>();

            foreach (var id in userIds)
            {
                var user = await _userRepository.FindUserByIdAsync(id);
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

           await _friendshipRepository.AddFriendshipAsync(friendship);
        }
    }
}
