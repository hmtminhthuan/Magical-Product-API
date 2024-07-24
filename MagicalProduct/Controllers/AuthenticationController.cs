using Microsoft.AspNetCore.Mvc;
using MagicalProduct.API.Controllers;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Payload.Request;

namespace SE171030.Lab03ProductManagement.API.Controllers
{
    [Route("/api/v1/auth")]
    [ApiController]
    public class AuthenticationController : BaseController<AuthenticationController>
    {
        private readonly IUserService _userService;
        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ProductRequest loginRequest)
        {
            var loginResponse = await _userService.Login(loginRequest);
            return new ObjectResult(loginResponse) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
