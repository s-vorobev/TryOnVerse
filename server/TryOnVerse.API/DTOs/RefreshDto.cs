namespace TryOnVerse.API.DTOs;

using System.ComponentModel.DataAnnotations;
using TryOnVerse.API.Common;

public class RefreshDto
{
    [Required]
    [MaxLength(DbConstants.RefreshToken.TokenMaxLength)]
    public string RefreshToken { get; set; } = null!;
}