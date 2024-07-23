using System;
using System.Collections.Generic;

namespace MagicalProduct.API.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Cards = new HashSet<Card>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? PaymentType { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
