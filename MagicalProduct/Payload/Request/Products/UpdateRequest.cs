using System.ComponentModel.DataAnnotations;

namespace MagicalProduct.API.Payload.Request.Products
{
    public class UpdateRequest : CreateRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;
    }
}
