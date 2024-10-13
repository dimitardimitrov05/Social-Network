using Connectly.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectly.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(CreatePost());
        }

        private Post CreatePost()
        {
            var post = new Post()
            {
                Id = Guid.Parse("afd42ca1-7fb8-46b8-a4c0-a2a11f3d77e9"),
                CreationOfPost = DateTime.Now,
                UserId = "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                Text = "Welcome to Connectly",
                Visibility = "Public"
            };
            return post;
        }
    }
}
