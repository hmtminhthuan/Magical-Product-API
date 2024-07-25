using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;
using System.Threading.Tasks;

namespace MagicalProduct.API.Services.Interfaces
{
    public interface IRoleService
    {
        Task<BasicResponse> GetAllRolesAsync();
        Task<BasicResponse> GetRoleByIdAsync(int id);
        Task<BasicResponse> CreateRoleAsync(CreateRoleRequest createRoleRequest);
        Task<BasicResponse> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest);
        Task<BasicResponse> DeleteRoleAsync(int id);
    }
}
