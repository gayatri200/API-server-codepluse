using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse1.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CodePulse1.Repository.Implementation
{
    public class CategoryRepository : IcategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var existingcategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id ==id);
            if (existingcategory is null)
            {
                return null;
            }
                dbContext.Categories.Remove(existingcategory);
                dbContext.SaveChangesAsync();

                return existingcategory;
            
           
        }

        public async Task<IEnumerable<Category>> GetallAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(int id)   //firstordefault will find id one by one if it finds then give id else return null
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<Category> UpdateAsync(Category category)
        {
           var existingcategory= await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
           if(existingcategory!=null)
            {
                dbContext.Entry(existingcategory).CurrentValues.SetValues(category);
                dbContext.SaveChanges();

                return category;
            }
            return null;
        }
    }
}
