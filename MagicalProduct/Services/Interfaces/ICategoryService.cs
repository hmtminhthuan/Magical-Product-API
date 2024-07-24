using MagicalProduct.API.Payload.Request.Categories;
using MagicalProduct.API.Payload.Response;
using System.Threading.Tasks;

namespace MagicalProduct.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<BasicResponse> SearchAsync();
        Task<BasicResponse> GetByIdAsync(int id);
        Task<BasicResponse> CreateAsync(CreateCategoryRequest request);
        Task<BasicResponse> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<BasicResponse> DeleteAsync(int id);
    }
}
