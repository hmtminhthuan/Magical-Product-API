using MagicalProduct.API.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace MagicalProduct.API.Payload.Request
{
    public class CreateOrderRequest
    {
        [FromForm(Name = "user-id")]
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [FromForm(Name = "total-amount")]
        [Required(ErrorMessage = "Total Amount is required")]
        public decimal TotalAmount { get; set; }

        [FromForm(Name = "address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [FromForm(Name = "payment-method-id")]
        public int PaymentMethodId { get; set; }

        [FromForm(Name = "order-details")]
        public ICollection<CreateOrderDetailRequest> OrderDetails { get; set; }
    }

    public class UpdateOrderRequest
    {
        [FromForm(Name = "id")]
        [Required(ErrorMessage = "Order Id is required")]
        public int Id { get; set; }

        [FromForm(Name = "user-id")]
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [FromForm(Name = "total-amount")]
        [Required(ErrorMessage = "Total Amount is required")]
        public decimal TotalAmount { get; set; }

        [FromForm(Name = "address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [FromForm(Name = "payment-method-id")]
        public int PaymentMethodId { get; set; }

        [FromForm(Name = "order-details")]
        public ICollection<UpdateOrderDetailRequest> OrderDetails { get; set; }
    }

    public class UpdateOrderStatusRequest
    {
        [JsonPropertyName("id")]
        [Required(ErrorMessage = "Order Id is required")]
        public int Id { get; set; }

        [JsonPropertyName("status")]
        [Required(ErrorMessage = "Status is required")]
        public OrderStatusEnum Status { get; set; }
    }

    public class CreateOrderDetailRequest
    {
        [FromForm(Name = "product-id")]
        public string ProductId { get; set; }

        [FromForm(Name = "amount")]
        public decimal Amount { get; set; }

        [FromForm(Name = "quantity")]
        public int Quantity { get; set; }
    }

    public class UpdateOrderDetailRequest
    {
        [FromForm(Name = "id")]
        public int Id { get; set; }

        [FromForm(Name = "product-id")]
        public string ProductId { get; set; }

        [FromForm(Name = "amount")]
        public decimal Amount { get; set; }

        [FromForm(Name = "quantity")]
        public int Quantity { get; set; }
    }
}
