namespace TryOnVerse.API.DTOs;

using System.ComponentModel.DataAnnotations;
using TryOnVerse.API.Common;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [MaxLength(DbConstants.User.EmailMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(DbConstants.User.PasswordMinLength)]
    public string Password { get; set; } = null!;
}