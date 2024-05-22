using CourseWork.Server.Interfaces;
using CourseWork.Server.Models;

namespace CourseWork.Server.Services
{
    public class AmazonCategoryService : IAmazonCategoryService
    {
        private readonly IAmazonCategoryRepository _categoryRepository;

        public AmazonCategoryService(IAmazonCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<AmazonCategory> AddCategoryAsync(AmazonCategory category)
        {
            if (await _categoryRepository.CategoryExistsAsync(category.Id))
            {
                throw new InvalidOperationException("Category already exists.");
            }
            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepository.CategoryExistsAsync(id);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<List<AmazonCategory>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync();
        }

        public async Task<AmazonCategory> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task UpdateCategoryAsync(int id, AmazonCategory category)
        {
            if (id != category.Id)
            {
                throw new ArgumentException("Id does not match category.Id");
            }
            await _categoryRepository.UpdateCategoryAsync(category);
        }
    }
}
