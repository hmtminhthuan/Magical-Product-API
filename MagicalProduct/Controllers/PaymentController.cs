using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Request.Payment;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Payload.Response.Payment;
using MagicalProduct.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicalProduct.API.Controllers
{
    [ApiController]
    [Route("api/payment-methods")]
    public class PaymentController : BaseController<PaymentController>
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService) : base(logger)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods(string? type)
        {
            var response = await _paymentService.GetPaymentMethods(type);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethodById(int id)
        {
            var response = await _paymentService.GetPaymentMethodById(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod(PaymentReq paymentMethod)
        {
            var response = await _paymentService.CreatePaymentMethod(paymentMethod);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod(PaymentResponse payment)
        {
            var response = await _paymentService.UpdatePaymentMethod(payment);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var response = await _paymentService.DeletePaymentMethod(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
