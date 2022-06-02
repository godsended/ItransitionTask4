using System.ComponentModel.DataAnnotations;

namespace ItransitionTask4.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Type your name")]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Type your email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Type your password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}