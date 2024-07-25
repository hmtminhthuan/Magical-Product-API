using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;

namespace MagicalProduct.API.Services.Interfaces
{
	public interface IUserService
	{
		Task<BasicResponse> Login(ProductRequest loginRequest);
        Task<BasicResponse> GetAllUsersAsync();
        Task<BasicResponse> GetUserByIdAsync(string id);
        Task<BasicResponse> CreateUserAsync(CreateUserRequest createUserRequest);
        Task<BasicResponse> UpdateUserAsync(UpdateUserRequest updateUserRequest);
        Task<BasicResponse> DeleteUserAsync(string id);
    }
}
