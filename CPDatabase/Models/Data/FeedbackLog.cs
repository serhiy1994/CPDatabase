using System;
using System.ComponentModel.DataAnnotations;

namespace CPDatabase.Models
{
    public partial class FeedbackLog
    {
        public int MessageId { get; set; } = default!;

        [Display(Name = "MessageDate")]
        public DateTime DateMessage { get; set; } = default!;

        [Required(ErrorMessage = "UsernameRequired")]
        [Display(Name = "Username")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "WrongEmail")]
        [Display(Name = "Email")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "MessageRequired")]
        [Display(Name = "Message")]
        public string Message { get; set; } = default!;

        [Display(Name = "Reply")]
        public string? Reply { get; set; }

        [Display(Name = "ReplyDate")]
        public DateTime? DateReply { get; set; }
    }
}