using CourseWork.Server.Models;

namespace CourseWork.Server.Interfaces
{
    public interface IAmazonCategoryRepository
    {
        Task<List<AmazonCategory>> GetCategoriesAsync();
        Task<AmazonCategory> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(AmazonCategory category);
        Task<AmazonCategory> AddCategoryAsync(AmazonCategory category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
    }
}
