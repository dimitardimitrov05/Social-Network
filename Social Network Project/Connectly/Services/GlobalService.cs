using Connectly.Contracts;
using Connectly.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Connectly.Services
{
    public class GlobalService : IGlobalService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GlobalService(IFriendshipRepository friendshipRepository, IHttpContextAccessor httpContextAccessor)
        {
            _friendshipRepository = friendshipRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsThereFriendRequests()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentException("Something went wrong!");
            }
            
            return _friendshipRepository.AreThereFriendRequests(userId);
        }
    }
}
