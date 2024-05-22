using CourseWork.Server.Interfaces;
using CourseWork.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Server.Repositories
{
    public class AmazonProductRepository : IAmazonProductRepository
    {
        private readonly CourseWorkContext _dataContext;
        
        public AmazonProductRepository(CourseWorkContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AmazonProduct> AddProductAsync(AmazonProduct product)
        {
            await _dataContext.AmazonProducts.AddAsync(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var product = await _dataContext.AmazonProducts.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            
            _dataContext.AmazonProducts.Remove(product);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<AmazonProduct> GetProductByIdAsync(string id)
        {
            return await _dataContext.AmazonProducts.FindAsync(id);
        }

        public async Task<List<AmazonProduct>> GetProductsAsync()
        {
            return await _dataContext.AmazonProducts
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<List<AmazonProduct>> GetProductsPerPage(int pageNumber, int pageSize, int? category, decimal? minPrice, decimal? maxPrice, bool? discounted, int? minReviews,
          decimal? minRating, int? minSoldLastMonth, string sortBy, string sortOrder)
        {
            var query = _dataContext.AmazonProducts.AsQueryable();

            if (category.HasValue)
                query = query.Where(p => p.CategoryId == category);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (discounted.HasValue && discounted.Value)
                query = query.Where(p => p.Price > p.ListPrice);

            if (minReviews.HasValue)
                query = query.Where(p => p.Reviews >= minReviews.Value);

            if (minRating.HasValue)
                query = query.Where(p => p.Stars >= minRating.Value);

            if (minSoldLastMonth.HasValue)
                query = query.Where(p => p.BoughtInLastMonth > minSoldLastMonth.Value);

            switch (sortBy.ToLower())
            {
                case "price":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                    break;
                case "discountedprice":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.ListPrice) : query.OrderByDescending(p => p.ListPrice);
                    break;
                case "reviews":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Reviews) : query.OrderByDescending(p => p.Reviews);
                    break;
                case "rating":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Stars) : query.OrderByDescending(p => p.Stars);
                    break;
                case "soldlastmonth":
                    query = sortOrder == "asc" ? query.OrderBy(p => p.BoughtInLastMonth) : query.OrderByDescending(p => p.BoughtInLastMonth);
                    break;
                default:
                    query = sortOrder == "asc" ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title);
                    break;
            }

            var amazonProducts = await query
                .Include(p => p.Category)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return amazonProducts;
        }

        public async Task<bool> ProductExistsAsync(string id)
        {
            return await _dataContext.AmazonProducts.AnyAsync(p => p.Asin == id);
        }

        public async Task UpdateProductAsync(string id, AmazonProduct product)
        {
            _dataContext.Entry(product).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
