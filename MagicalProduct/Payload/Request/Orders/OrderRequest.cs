using MagicalProduct.API.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace MagicalProduct.API.Payload.Request
{
    public class CreateOrderRequest
    {
        [JsonPropertyName("user-id")]
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [JsonPropertyName("total-amount")]
        [Required(ErrorMessage = "Total Amount is required")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [JsonPropertyName("payment-method-id")]
        public int PaymentMethodId { get; set; }

        [JsonPropertyName("order-details")]
        public ICollection<CreateOrderDetailRequest> OrderDetails { get; set; }
    }

    public class UpdateOrderRequest
    {
        [JsonPropertyName("id")]
        [Required(ErrorMessage = "Order Id is required")]
        public int Id { get; set; }

        [JsonPropertyName("user-id")]
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [JsonPropertyName("total-amount")]
        [Required(ErrorMessage = "Total Amount is required")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [JsonPropertyName("payment-method-id")]
        public int PaymentMethodId { get; set; }

        [JsonPropertyName("order-details")]
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
        [JsonPropertyName("product-id")]
        public string ProductId { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    public class UpdateOrderDetailRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("product-id")]
        public string ProductId { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
