using CourseWork.Server.Interfaces;
using CourseWork.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Server.Repositories
{
    public class AmazonCategoryRepository : IAmazonCategoryRepository
    {
        private readonly CourseWorkContext _dataContext;
        public AmazonCategoryRepository(CourseWorkContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<List<AmazonCategory>> GetCategoriesAsync()
        {
            return await _dataContext.AmazonCategories.ToListAsync();
        }

        public async Task<AmazonCategory> GetCategoryByIdAsync(int id)
        {
            return await _dataContext.AmazonCategories.FindAsync(id);
        }

        public async Task UpdateCategoryAsync(AmazonCategory category)
        {
            _dataContext.Entry(category).State = EntityState.Modified;  
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dataContext.AmazonCategories.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            _dataContext.AmazonCategories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<AmazonCategory> AddCategoryAsync(AmazonCategory category)
        {
            await _dataContext.AmazonCategories.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _dataContext.AmazonCategories.AnyAsync(c => c.Id == id);
        }
    }
}
