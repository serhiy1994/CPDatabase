﻿using System;
using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class User
    {
        public int Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}