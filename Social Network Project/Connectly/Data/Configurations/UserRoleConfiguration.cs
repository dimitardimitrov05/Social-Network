using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectly.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(CreateUserRole());
        }

        private HashSet<IdentityUserRole<string>> CreateUserRole()
        {
            return new HashSet<IdentityUserRole<string>>
            {
                new()
                {
                    UserId = "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                    RoleId = "528726ea-e421-4a80-b303-f035355599de"
                },
            };
        }
    }
}
