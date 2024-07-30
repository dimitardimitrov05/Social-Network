using Connectly.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectly.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.UserName)
                .IsUnique(true);

            builder.HasIndex(x => x.Email)
                .IsUnique(true);

            builder.HasMany(x => x.Invitations)
                .WithOne(x => x.CreatorUser)
                .HasForeignKey(x => x.UserCreatedTheInvite);


            builder.HasMany(x => x.UserFriendships)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasData(CreateAdmin());
        }

        private User CreateAdmin()
        {
            var user = new User()
            {
                Id = "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                FirstName = "Dimitar",
                LastName = "Dimitrov",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmai.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                DateOfBirth = new DateTime(2005, 5, 10),
                Gender = "Male",
                AccountPrivacy = "Public"
            };

            var hasher = new PasswordHasher<User>();

            user.PasswordHash =
                hasher.HashPassword(user, "admin");

            return user;
        }
    }
}
