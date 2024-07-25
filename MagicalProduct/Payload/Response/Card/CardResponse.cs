namespace MagicalProduct.API.Payload.Response.Card
{
    public class CardResponse
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? NameOnCard { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? PaymentMethodId { get; set; }
        public string? PaymentType { get; set; }
        public string? UserName { get; set; }
    }
}
