namespace TryOnVerse.API.DTOs;

using System.ComponentModel.DataAnnotations;

public class RegisterUserDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(12)]
    public string Password { get; set; } = null!;

    [MaxLength(20)]
    public string Role { get; set; } = "Customer";
}