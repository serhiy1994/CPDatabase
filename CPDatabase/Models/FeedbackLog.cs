﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CPDatabase.Models
{
    public partial class FeedbackLog
    {
        public int MessageId { get; set; } = default!;
        public DateTime DateMessage { get; set; } = default!;
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Message { get; set; } = default!;
        public string? Reply { get; set; }
        public DateTime? DateReply { get; set; }
    }
}