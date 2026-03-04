namespace TryOnVerse.API.DTOs;

using System.ComponentModel.DataAnnotations;
using TryOnVerse.API.Common;

public class RegisterCustomerDto
{
    [Required]
    [MaxLength(DbConstants.User.FirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(DbConstants.User.LastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(DbConstants.User.EmailMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(DbConstants.User.PasswordMinLength)]
    public string Password { get; set; } = null!;
}