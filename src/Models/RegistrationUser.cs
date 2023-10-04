using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace src.Models
{
    public class RegistrationUser
    {
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Too short Name")]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "Not allowed character")]
        [Remote(action: "CheckUseraname", controller: "Helper", ErrorMessage ="Username taken")]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        [Remote(action: "CheckEmail", controller: "Helper", ErrorMessage ="Email already in use")]
        public string Email { get; set; } = null!;
        [Required]
        // depends on country
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Too short number")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Not allowed character")]
        public string? Phone { get; set; }
    }
}