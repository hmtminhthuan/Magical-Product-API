using Microsoft.AspNetCore.Mvc;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.API.Enums;
using System;
using System.Threading.Tasks;
using MagicalProduct.API.Middlewares;

namespace MagicalProduct.API.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : BaseController<OrderController>
    {
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
            : base(logger)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderService.GetAllOrdersAsync();
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPost]
        [AuthorizePolicy(RoleEnum.Admin, RoleEnum.Staff)]
        public async Task<IActionResult> CreateOrder([FromForm] CreateOrderRequest createOrderRequest)
        {
            var response = await _orderService.CreateOrderAsync(createOrderRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPut("{id}")]
        [AuthorizePolicy(RoleEnum.Admin, RoleEnum.Staff)]
        public async Task<IActionResult> UpdateOrder(int id, [FromForm] UpdateOrderRequest updateOrderRequest)
        {
            updateOrderRequest.Id = id;
            var response = await _orderService.UpdateOrderAsync(updateOrderRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPatch("update-status")]
        [AuthorizePolicy(RoleEnum.Admin, RoleEnum.Staff)]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest updateOrderStatusRequest)
        {
            var response = await _orderService.UpdateOrderStatusAsync(updateOrderStatusRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
