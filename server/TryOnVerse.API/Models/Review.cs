namespace TryOnVerse.API.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Review
{
    public int ReviewID { get; set; }

    [Required]
    public int UserID { get; set; }

    [Required]
    public int ClothingID { get; set; }

    [Required]
    public int Rating { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
    public Clothing Clothing { get; set; } = null!;
}