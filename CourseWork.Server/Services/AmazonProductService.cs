using CourseWork.Server.Interfaces;
using CourseWork.Server.Models;

namespace CourseWork.Server.Services
{
    public class AmazonProductService : IAmazonProductService
    {
        private readonly IAmazonProductRepository _productRepository;
        public AmazonProductService(IAmazonProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task<AmazonProduct> AddProductAsync(AmazonProduct product)
        {
            if (await _productRepository.ProductExistsAsync(product.Asin))
            {
                throw new InvalidOperationException("Product already exists");
            }
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<AmazonProduct> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<List<AmazonProduct>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<List<AmazonProduct>> GetProductsPerPage(int pageNumber, int pageSize, int? category, decimal? minPrice, decimal? maxPrice, bool? discounted, int? minReviews, decimal? minRating, int? minSoldLastMonth, string sortBy, string sortOrder)
        {
            return await _productRepository.GetProductsPerPage(pageNumber, pageSize, category, minPrice, maxPrice, discounted, minReviews, minRating, minSoldLastMonth, sortBy, sortOrder);
        }

        public async Task<bool> ProductExistsAsync(string id)
        {
            return await _productRepository.ProductExistsAsync(id);
        }

        public Task UpdateProductAsync(string id, AmazonProduct product)
        {
            if (id != product.Asin)
            {
                throw new ArgumentException("Id does not match product.Asin");
            }
            return _productRepository.UpdateProductAsync(id, product);
        }
    }
}
