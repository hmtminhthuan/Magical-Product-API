using MagicalProduct.API.Payload.Request.Products;
using MagicalProduct.API.Payload.Response;
using System.Threading.Tasks;

namespace MagicalProduct.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<BasicResponse> SearchAsync(GetProductRequest getProductRequest);
        Task<BasicResponse> GetByIdAsync(string id);
        Task<BasicResponse> CreateAsync(CreateRequest createReq);
        Task<BasicResponse> UpdateAsync(UpdateRequest updateReq);
        Task<BasicResponse> DeleteAsync(string id);
    }
}
