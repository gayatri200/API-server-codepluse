using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse1.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse1.Repository.Implementation
{
    public class BlogPostRepository : IblogpostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetallAsync()
        {
          return  await dbContext.BlogPosts.Include(x=>x.categories).ToListAsync();
        }
    }
}
