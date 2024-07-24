using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;

namespace MagicalProduct.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BasicResponse> GetAllOrdersAsync();
        Task<BasicResponse> GetOrderByIdAsync(int id);
        Task<BasicResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest);
        Task<BasicResponse> UpdateOrderAsync(UpdateOrderRequest updateOrderRequest);
        Task<BasicResponse> UpdateOrderStatusAsync(UpdateOrderStatusRequest updateOrderStatusRequest);
    }
}
