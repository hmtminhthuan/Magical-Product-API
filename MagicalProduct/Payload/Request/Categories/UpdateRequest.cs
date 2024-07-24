using System.ComponentModel.DataAnnotations;

namespace MagicalProduct.API.Payload.Request.Categories
{
    public class UpdateRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
    }
}
