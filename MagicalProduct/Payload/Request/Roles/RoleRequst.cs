using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MagicalProduct.API.Payload.Request
{
    public class CreateRoleRequest
    {
        [FromForm(Name = "name")]
        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
    }

    public class UpdateRoleRequest
    {
        [FromForm(Name = "id")]
        [Required(ErrorMessage = "Role Id is required")]
        public int Id { get; set; }

        [FromForm(Name = "name")]
        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
    }
}
