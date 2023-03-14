using System.ComponentModel.DataAnnotations;

namespace CPDatabase.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Name isn't specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password isn't specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
