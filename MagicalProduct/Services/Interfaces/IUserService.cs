using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;

namespace MagicalProduct.API.Services.Interfaces
{
	public interface IUserService
	{
		Task<BasicResponse> Login(ProductRequest loginRequest);
	}
}
