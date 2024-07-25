using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MagicalProduct.API.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public bool? Status { get; set; }
        [JsonIgnore]

        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
