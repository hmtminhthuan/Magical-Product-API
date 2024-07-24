using System.ComponentModel.DataAnnotations;

namespace MagicalProduct.API.Payload.Request.Categories
{
    public class CreateRequest
    {
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(50, ErrorMessage = "Category name's max length is 50 characters")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
