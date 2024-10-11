using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _context;

        public InvitationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddInvitationAsync(Invitation invitation)
        {
            await _context.Invitations.AddAsync(invitation);
            await _context.SaveChangesAsync();
        }

        public async Task<Invitation> FindInvitationByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
