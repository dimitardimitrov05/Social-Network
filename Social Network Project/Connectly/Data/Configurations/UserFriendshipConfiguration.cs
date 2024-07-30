using Connectly.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectly.Data.Configurations
{
    public class UserFriendshipConfiguration : IEntityTypeConfiguration<UserFriendship>
    {
        public void Configure(EntityTypeBuilder<UserFriendship> builder)
        {
            builder.HasKey(x => new { x.UserId, x.FriendshipId });
        }
    }
}
