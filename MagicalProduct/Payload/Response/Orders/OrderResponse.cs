using System.Text.Json.Serialization;

namespace MagicalProduct.API.Payload.Response
{
    public class OrderResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user-id")]
        public string UserId { get; set; }

        [JsonPropertyName("total-amount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("create-at")]
        public DateTime CreateAt { get; set; }

        [JsonPropertyName("payment-method-id")]
        public int PaymentMethodId { get; set; }

        [JsonPropertyName("order-details")]
        public ICollection<OrderDetailResponse> OrderDetails { get; set; }
    }

    public class OrderDetailResponse
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