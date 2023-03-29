using System;
using System.ComponentModel.DataAnnotations;

namespace CPDatabase.Models
{
    public partial class FeedbackInputModel
    {
        [Required(ErrorMessage = "UsernameRequired")]
        [Display(Name = "Username")]
        public string? Username { get; set; } = default!;

        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "WrongEmail")]
        [Display(Name = "Email")]
        public string? Email { get; set; } = default!;

        [Required(ErrorMessage = "MessageRequired")]
        [Display(Name = "Message")]
        public string? Message { get; set; } = default!;

        [Required]
        public string? ReturnUrl { get; set; } = default!;
    }
}