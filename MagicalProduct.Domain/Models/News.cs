using System;
using System.Collections.Generic;

namespace MagicalProduct.API.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Thumbnail { get; set; }
        public string? Content { get; set; }
    }
}
