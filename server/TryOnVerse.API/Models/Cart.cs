namespace TryOnVerse.API.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Cart
{
    public int CartID { get; set; }

    [Required]
    public int UserID { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}