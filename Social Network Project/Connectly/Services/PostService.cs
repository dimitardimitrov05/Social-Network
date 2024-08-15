using Connectly.Contracts;
using Connectly.Data;
using Connectly.Data.Account;
using Connectly.Data.Entities;
using Connectly.Models.PostViewModels;
using Microsoft.EntityFrameworkCore;

namespace Connectly.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePostAsync(IndexViewModel model, User user)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                CreationOfPost = DateTime.Now,
                UserId = user.Id,
                Text = model.PostContent!,
                Visibility = model.PostVisibility!
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PostViewModel>> ListPosts()
        {
            return await _context.Posts.Include(x => x.User)
                .Select(x => new PostViewModel
                {
                    Text = x.Text,
                    UserFirstName = x.User.FirstName,
                    UserLastName = x.User.LastName,
                    CreationOfPost = x.CreationOfPost,
                    Visibility = x.Visibility,
                }).ToListAsync();
        }
    }
}
