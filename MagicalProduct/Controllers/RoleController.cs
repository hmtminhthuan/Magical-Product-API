using Microsoft.AspNetCore.Mvc;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Services.Interfaces;
using System.Threading.Tasks;
using MagicalProduct.API.Enums;
using MagicalProduct.API.Middlewares;

namespace MagicalProduct.API.Controllers
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RoleController : BaseController<RoleController>
    {
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
            : base(logger)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRolesAsync();
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _roleService.GetRoleByIdAsync(id);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPost]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> CreateRole(CreateRoleRequest createRoleRequest)
        {
            var response = await _roleService.CreateRoleAsync(createRoleRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPut("{id}")]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> UpdateRole(int id, UpdateRoleRequest updateRoleRequest)
        {
            updateRoleRequest.Id = id;
            var response = await _roleService.UpdateRoleAsync(updateRoleRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpDelete("{id}")]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteRoleAsync(id);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
