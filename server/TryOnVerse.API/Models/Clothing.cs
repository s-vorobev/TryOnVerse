namespace TryOnVerse.API.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum ClothingType
{
    Top,
    Bottom
}

public class Clothing
{
    public int ClothingID { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Required, MaxLength(50)]
    public string Category { get; set; } = null!;

    [Required]
    public ClothingType ClothingType { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int StockQuantity { get; set; } = 0;

    [MaxLength(20)]
    public string Size { get; set; } = null!;

    [MaxLength(30)]
    public string Color { get; set; } = null!;

    [MaxLength(255)]
    public string ImageURL { get; set; } = null!;

    [MaxLength(255)]
    public string Model3DURL { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}