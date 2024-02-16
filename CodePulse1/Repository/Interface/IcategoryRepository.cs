using CodePulse.API.Models.Domain;

namespace CodePulse1.Repository.Interface
{
    public interface IcategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task <IEnumerable<Category>> GetallAsync();
        Task<Category?> GetById(int id);
    

        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteCategory(int id);
    }
}
