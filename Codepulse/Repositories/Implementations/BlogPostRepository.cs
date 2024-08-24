using Codepulse.Data;
using Codepulse.Models.Domain;
using Codepulse.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Codepulse.Repositories.Implementations
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
          return  await  _dbContext.BlogPosts.ToListAsync();
        }
    }
}
