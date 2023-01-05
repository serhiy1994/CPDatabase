using System;
using System.Collections.Generic;

namespace CPDatabase.Models
{
    public partial class FeedbackLog
    {
        public int MessageId { get; set; } = default!;
        public DateTime DateMessage { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string? Reply { get; set; }
        public DateTime? DateReply { get; set; }
    }
}