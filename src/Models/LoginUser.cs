using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class LoginUser
    {
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Too short Name")]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "Not allowed character")]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}