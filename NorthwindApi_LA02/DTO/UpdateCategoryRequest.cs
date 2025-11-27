using System.ComponentModel.DataAnnotations;

namespace NorthwindApi_LA02.DTO
{
    public class UpdateCategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
