using Microsoft.AspNetCore.Mvc;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Services.Interfaces;
using System.Threading.Tasks;
using MagicalProduct.API.Enums;
using MagicalProduct.API.Middlewares;

namespace MagicalProduct.API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
            : base(logger)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPost]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserRequest createUserRequest)
        {
            var response = await _userService.CreateUserAsync(createUserRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPut("{id}")]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserRequest updateUserRequest)
        {
            updateUserRequest.Id = id;
            var response = await _userService.UpdateUserAsync(updateUserRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpDelete("{id}")]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
