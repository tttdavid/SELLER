using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace src.Models
{
    public class EmailModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        [Remote(action: "CheckEmail", controller: "Helper", ErrorMessage ="Email already in use")]
        public string Email { get; set; } = null!;
    }
}