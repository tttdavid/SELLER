using System;

namespace src.ViewModels
{
    public class UserView
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Avatar { get; set; }
    }
}
