using System;
using System.Collections.Generic;

namespace MagicalProduct.API.Models
{
    public partial class User
    {
        public User()
        {
            Cards = new HashSet<Card>();
            Orders = new HashSet<Order>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? Gender { get; set; }
        public bool? Status { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
