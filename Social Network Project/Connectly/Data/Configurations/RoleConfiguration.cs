using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectly.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(CreateRole());
        }

        private HashSet<IdentityRole> CreateRole()
        {
            return new HashSet<IdentityRole>
            {
                new()
                {
                    Id = "528726ea-e421-4a80-b303-f035355599de",
                    Name = "Admin",
                    NormalizedName= "ADMIN",
                },
                new()
                {
                    Id = "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                    Name = "User",
                    NormalizedName= "USER",
                }
            };
        }
    }
}
