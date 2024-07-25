using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MagicalProduct.API.Payload.Request
{
    public class CreateUserRequest
    {
        [FromForm(Name = "name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [FromForm(Name = "phone")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [FromForm(Name = "email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [FromForm(Name = "password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [FromForm(Name = "gender")]
        public bool? Gender { get; set; }

        [FromForm(Name = "status")]
        public bool? Status { get; set; }

        [FromForm(Name = "role-id")]
        public int? RoleId { get; set; }
    }

    public class UpdateUserRequest
    {
        [FromForm(Name = "id")]
        [Required(ErrorMessage = "User Id is required")]
        public string Id { get; set; }

        [FromForm(Name = "name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [FromForm(Name = "phone")]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [FromForm(Name = "email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [FromForm(Name = "password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [FromForm(Name = "gender")]
        public bool? Gender { get; set; }

        [FromForm(Name = "status")]
        public bool? Status { get; set; }

        [FromForm(Name = "role-id")]
        public int? RoleId { get; set; }
    }
}