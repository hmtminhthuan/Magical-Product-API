using MagicalProduct.API.Payload.Request.Card;
using MagicalProduct.API.Payload.Response;

namespace MagicalProduct.API.Services.Interfaces
{
    public interface ICardService
    {
        BasicResponse Get(string? name, string? cardNum);
        BasicResponse GetById(int id);
        Task<BasicResponse> Create(CardReq req);
        Task<BasicResponse> Update(int id, CardReq cardReq);
        Task<BasicResponse> Delete(int id);
    }
}
