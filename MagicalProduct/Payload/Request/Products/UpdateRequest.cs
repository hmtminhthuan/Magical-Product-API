using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MagicalProduct.API.Payload.Request.Products
{
    public class UpdateRequest
    {
        [FromForm(Name = "id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

        [FromForm(Name = "name")]
        [MaxLength(50, ErrorMessage = "Product name's max length is 50 characters")]
        public string? Name { get; set; }

        [FromForm(Name = "description")]
        public string? Description { get; set; }

        [FromForm(Name = "price")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal? Price { get; set; }

        [FromForm(Name = "image-file")]
        public IFormFile? ImageFile { get; set; }

        [FromForm(Name = "status")]
        public bool? Status { get; set; }

        [FromForm(Name = "category-id")]
        public int? CategoryId { get; set; }
    }
}
