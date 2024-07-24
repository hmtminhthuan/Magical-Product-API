using System.ComponentModel.DataAnnotations;

namespace MagicalProduct.API.Payload.Request.Products
{
    public class CreateRequest
    {
        
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        //[Required]
        //public decimal? Discount { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public bool? Status { get; set; } 
        [Required]
        public int? CategoryId { get; set; }
    }
}
