using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindApi_LA02.Data;
using NorthwindApi_LA02.Domain;
using NorthwindApi_LA02.DTO;

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
        [Authorize]
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryRequest request) 
        {
            //Mapping
            var category = new Category { 
                CategoryId = request.CategoryId,
                CategoryName = request.CategoryName,
                Description = request.Description
            };
            _context.Categories.Add(category);//memoria művelet
            _context.SaveChanges();//adatbázis mentés!!!!
            return Created(string.Empty, null);
        
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(short id, [FromBody] UpdateCategoryRequest request) 
        {
            var category = _context.Categories
                .SingleOrDefault(c => c.CategoryId == id);

            if (category is null)
                return NotFound();

            category.CategoryName = request.CategoryName;
            category.Description = request.Description;

            _context.Update(category);
            _context.SaveChanges();

            return Ok();
        
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(short id) 
        {
            var category = _context.Categories
                .SingleOrDefault(c => c.CategoryId == id);
            
            if ( category is null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();

        
        }
        

    }
}
