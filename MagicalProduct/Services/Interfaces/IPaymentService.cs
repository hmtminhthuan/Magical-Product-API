using MagicalProduct.API.Payload.Request.Payment;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Payload.Response.Payment;

namespace MagicalProduct.API.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BasicResponse> GetPaymentMethods(string? type);
        Task<BasicResponse> GetPaymentMethodById(int id);
        Task<BasicResponse> CreatePaymentMethod(PaymentReq request);
        Task<BasicResponse> UpdatePaymentMethod(PaymentResponse request);
        Task<BasicResponse> DeletePaymentMethod(int id);
    }
}
