namespace MagicalProduct.API.Payload.Request.Card
{
    public class CardReq
    {
        public string? UserId { get; set; }
        public string? NameOnCard { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? PaymentMethodId { get; set; }
    }
}
