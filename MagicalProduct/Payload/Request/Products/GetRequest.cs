namespace MagicalProduct.API.Payload.Request.Products
{
    public class GetRequest : CreateRequest
    {
        public string Id { get; set; } = null!;

        public string CategoryName { get; set; } = null!;
    }
}
