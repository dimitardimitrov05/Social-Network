using Connectly.Contracts;
using Connectly.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Connectly.Services
{
    public class GlobalService : IGlobalService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GlobalService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsThereFriendRequests()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            return _context.Friendships.Any(x => x.UserThatAcceptedOrDeclinedTheFriendship ==  userId && x.StatusOfFriendship == "Waiting");
        }
    }
}
