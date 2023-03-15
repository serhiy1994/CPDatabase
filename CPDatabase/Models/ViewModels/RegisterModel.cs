using System.ComponentModel.DataAnnotations;

namespace CPDatabase.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name isn't specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password isn't specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}