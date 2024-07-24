using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;
using System.Threading.Tasks;

namespace MagicalProduct.API.Services.Interfaces
{
    public interface INewsService
    {
        Task<BasicResponse> GetAllNewsAsync();
        Task<BasicResponse> GetNewsByIdAsync(int id);
        Task<BasicResponse> CreateNewsAsync(CreateNewsRequest createNewsRequest);
        Task<BasicResponse> UpdateNewsAsync(UpdateNewsRequest updateNewsRequest);
        Task<BasicResponse> DeleteNewsAsync(int id);
    }
}
