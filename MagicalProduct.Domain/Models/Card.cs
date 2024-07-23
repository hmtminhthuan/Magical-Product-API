using System;
using System.Collections.Generic;

namespace MagicalProduct.API.Models
{
    public partial class Card
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? NameOnCard { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? PaymentMethodId { get; set; }

        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual User? User { get; set; }
    }
}
