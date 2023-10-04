using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class AddProduct
    {
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Too short Title")]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "Not allowed character")]
        public string Title { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[\d+\.\d]+$", ErrorMessage = "Not allowed character")]
        public decimal? Price { get; set; }
        [Required]
        public IFormFile? Image { get; set; }

    }
}