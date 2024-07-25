using System.ComponentModel.DataAnnotations;

namespace MagicalProduct.API.Payload.Request.Categories
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
