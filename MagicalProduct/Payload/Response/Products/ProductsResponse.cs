namespace MagicalProduct.API.Payload.Response.Products
{
    public class ProductsResponse
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public object? result { get; set; }
    }
}
