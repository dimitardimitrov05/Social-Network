using Connectly.Contracts;
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
    }
}
