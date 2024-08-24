using Codepulse.Models.Domain;

namespace Codepulse.Repositories.Interface
{
    public interface IBlogPostRepository
    {
       Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();
    }
}
