using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItransitionTask4.Models;

[Serializable]
[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? LastLoginDateTime { get; set; } = null!;
    public string? RegistrationDateTime { get; set; } = null!;
    public UserState? State { get; set; } = null!;
    public string? LoginKey { get; set; }
}

[Serializable]
public enum UserState
{
    Common,
    Blocked
}