namespace TryOnVerse.API.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
    [Key]
    public int ReviewID { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey(nameof(UserID))]
    public User User { get; set; } = null!;

    [Required]
    public int ClothingID { get; set; }

    [ForeignKey(nameof(ClothingID))]
    public Clothing Clothing { get; set; } = null!;

    [Required]
    public int Rating { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}