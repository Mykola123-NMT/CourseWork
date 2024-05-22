using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseWork.Server.Models;
using CourseWork.Server.Interfaces;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.MinimalApi;
using MLModel_WebApi;

namespace CourseWork.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmazonProductsController : ControllerBase
    {
        private readonly IAmazonProductService _productService;
        private readonly IAmazonBestSellerAnalyzerService _bestSellerAnalyzerService;

        public AmazonProductsController(IAmazonProductService productService, IAmazonBestSellerAnalyzerService bestSellerAnalyzerService)
        {
            _productService = productService;
            _bestSellerAnalyzerService = bestSellerAnalyzerService;
        }

        // GET: api/AmazonProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmazonProduct>>> GetAmazonProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        // GET: api/AmazonProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmazonProduct>> GetAmazonProduct(string id)
        {
            var amazonProduct = await _productService.GetProductByIdAsync(id);

            if (amazonProduct == null)
            {
                return NotFound();
            }

            return amazonProduct;
        }

        // GET: api/AmazonProducts/PerPage/5/5
        [HttpGet("PerPage")]
        public async Task<ActionResult<IEnumerable<AmazonProduct>>> GetAmazonProductsPerPage(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] int? category = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] bool? discounted = null,
            [FromQuery] int? minReviews = null,
            [FromQuery] decimal? minRating = null,
            [FromQuery] int? minSoldLastMonth = null,
            [FromQuery] string sortBy = "title",
            [FromQuery] string sortOrder = "asc")
        {
            var amazonProducts = await _productService.GetProductsPerPage(pageNumber, pageSize, category, minPrice, maxPrice, discounted, minReviews, minRating, minSoldLastMonth, sortBy, sortOrder);

            return amazonProducts;
        }

        // GET: api/AmazonProducts/BestSellerAnalizer
        [HttpGet("BestSellerAnalizer/{id}")]
        public async Task<ActionResult<MLModel.ModelOutput>> GetAmazonProductBestSellerAnalizer(string id)
        {
            var amazonProduct = await _productService.GetProductByIdAsync(id);

            if (amazonProduct == null)
            {
                return NotFound();
            }

            var amazonProductsIsBestSeller = _bestSellerAnalyzerService.GetProductBestSellerAnalysis(amazonProduct);

            return amazonProductsIsBestSeller;
        }

        // PUT: api/AmazonProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmazonProduct(string id, AmazonProduct amazonProduct)
        {
            try
            {
                await _productService.UpdateProductAsync(id, amazonProduct);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _productService.ProductExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AmazonProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AmazonProduct>> PostAmazonProduct(AmazonProduct amazonProduct)
        {
            try
            {
                var newProduct = await _productService.AddProductAsync(amazonProduct);
                return CreatedAtAction("GetAmazonProduct", new { id = newProduct.Asin }, newProduct);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE: api/AmazonProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmazonProduct(string id)
        {
            var amazonProduct = await _productService.DeleteProductAsync(id);
            if (!amazonProduct)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
