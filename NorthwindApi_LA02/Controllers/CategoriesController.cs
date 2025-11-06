using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApi_LA02.Data;
using NorthwindApi_LA02.Domain;

namespace NorthwindApi_LA02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories;

            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public Category GetCategoryById(int id) {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        [HttpGet("CategoryCount")]
        public int GetCategoryCount() => _context.Categories.Count();

        [HttpGet("{id}/products")]
        public IEnumerable<Product> GetProductsById(int id) {
            return _context.Products.Where(p => p.CategoryId == id);
        }
    }
}
