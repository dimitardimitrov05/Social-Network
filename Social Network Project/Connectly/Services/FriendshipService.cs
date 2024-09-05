﻿using Connectly.Contracts;
using Connectly.Data;
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

        public async Task AcceptFriendRequest(string currentUserId, string otherUserId)
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

        public async Task DeclineFriendRequest(string currentUserId, string otherUserId)
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
