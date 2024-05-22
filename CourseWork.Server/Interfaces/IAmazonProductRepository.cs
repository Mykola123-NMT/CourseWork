using CourseWork.Server.Models;

namespace CourseWork.Server.Interfaces
{
    public interface IAmazonProductRepository
    {
        Task<List<AmazonProduct>> GetProductsAsync();
        Task<AmazonProduct> GetProductByIdAsync(string id);
        Task<List<AmazonProduct>> GetProductsPerPage(int pageNumber, int pageSize, int? category, decimal? minPrice, decimal? maxPrice,
            bool? discounted, int? minReviews, decimal? minRating, int? minSoldLastMonth, string sortBy, string sortOrder);
        Task UpdateProductAsync(string id, AmazonProduct product);
        Task<AmazonProduct> AddProductAsync(AmazonProduct product);
        Task<bool> DeleteProductAsync(string id);
        Task<bool> ProductExistsAsync(string id);
    }
}
