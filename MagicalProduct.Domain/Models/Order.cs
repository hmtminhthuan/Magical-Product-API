using System;
using System.Collections.Generic;

namespace MagicalProduct.API.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? UserId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? PaymentMethodId { get; set; }

        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
