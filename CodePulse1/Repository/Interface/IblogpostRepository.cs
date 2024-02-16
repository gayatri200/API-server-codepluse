using CodePulse.API.Models.Domain;

namespace CodePulse1.Repository.Interface
{
    public interface IblogpostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetallAsync();
    }
}
