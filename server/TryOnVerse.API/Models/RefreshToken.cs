namespace TryOnVerse.API.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TryOnVerse.API.Common;

public class RefreshToken
{
    public int TokenId { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey(nameof(UserID))]
    public User User { get; set; } = null!;

    [Required]
    [MaxLength(DbConstants.RefreshToken.TokenMaxLength)]
    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime ExpiresAt { get; set; }

    [Required]
    public bool IsRevoked { get; set; } = false;
}