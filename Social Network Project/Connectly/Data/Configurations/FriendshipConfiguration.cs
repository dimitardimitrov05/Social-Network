using Connectly.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectly.Data.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasMany(x => x.UserFriendships)
                .WithOne(x => x.Friendship)
                .HasForeignKey(x => x.FriendshipId);
        }
    }
}
