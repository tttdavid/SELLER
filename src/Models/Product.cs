using System;
using System.Collections.Generic;

namespace src.Models
{
    public partial class Product
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Type { get; set; } = null!;

        public decimal? Price { get; set; }

        public string? Image { get; set; }

        public int? AuthorId { get; set; }
    }

}