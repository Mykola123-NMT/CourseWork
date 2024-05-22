using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseWork.Server.Models;

namespace CourseWork.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmazonCategoriesController : ControllerBase
    {
        private readonly CourseWorkContext _context;

        public AmazonCategoriesController(CourseWorkContext context)
        {
            _context = context;
        }

        // GET: api/AmazonCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmazonCategory>>> GetAmazonCategories()
        {
            return await _context.AmazonCategories.ToListAsync();
        }

        // GET: api/AmazonCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmazonCategory>> GetAmazonCategory(int id)
        {
            var amazonCategory = await _context.AmazonCategories.FindAsync(id);

            if (amazonCategory == null)
            {
                return NotFound();
            }

            return amazonCategory;
        }

        // PUT: api/AmazonCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmazonCategory(int id, AmazonCategory amazonCategory)
        {
            if (id != amazonCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(amazonCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmazonCategoryExists(id))
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

        // POST: api/AmazonCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AmazonCategory>> PostAmazonCategory(AmazonCategory amazonCategory)
        {
            _context.AmazonCategories.Add(amazonCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AmazonCategoryExists(amazonCategory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAmazonCategory", new { id = amazonCategory.Id }, amazonCategory);
        }

        // DELETE: api/AmazonCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmazonCategory(int id)
        {
            var amazonCategory = await _context.AmazonCategories.FindAsync(id);
            if (amazonCategory == null)
            {
                return NotFound();
            }

            _context.AmazonCategories.Remove(amazonCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmazonCategoryExists(int id)
        {
            return _context.AmazonCategories.Any(e => e.Id == id);
        }
    }
}
