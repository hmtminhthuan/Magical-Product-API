using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MagicalProduct.API.Payload.Request.Products
{
    public class CreateRequest
    {
        [FromForm(Name = "name")]
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(50, ErrorMessage = "Product name's max length is 50 characters")]
        public string Name { get; set; }

        [FromForm(Name = "description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [FromForm(Name = "price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }

        [FromForm(Name = "image-file")]
        public IFormFile ImageFile { get; set; }

        [FromForm(Name = "status")]
        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

        [FromForm(Name = "category-id")]
        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; set; }
    }
}
