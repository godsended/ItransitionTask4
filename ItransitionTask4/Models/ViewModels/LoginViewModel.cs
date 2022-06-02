using System.ComponentModel.DataAnnotations;

namespace ItransitionTask4.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Type your email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Type your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}