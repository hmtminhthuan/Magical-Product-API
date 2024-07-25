using System.ComponentModel.DataAnnotations;
using MagicalProduct.API.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicalProduct.API.Payload.Request.Products
{

    public class GetRequest : CreateRequest
    {
        public string Id { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }

    public class GetProductRequest
    {
        public string SearchName { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? ProductStatus { get; set; }
        public string OrderFields { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }

    public class GetProductResponse
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool? First { get; set; }
        public bool? Last { get; set; }
        public List<Product> Data { get; set; }
    }
}
