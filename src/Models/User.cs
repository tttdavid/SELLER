﻿using System;
using System.Collections.Generic;

namespace src.Models
{
    public partial class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Avatar { get; set; }
    }
}
