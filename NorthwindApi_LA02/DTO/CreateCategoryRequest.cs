using System.ComponentModel.DataAnnotations;

namespace NorthwindApi_LA02.DTO
{
    public class CreateCategoryRequest
    {
        [Required]
        public short CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
