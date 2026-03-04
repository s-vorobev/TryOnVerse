namespace TryOnVerse.API.Data;

using System;
using System.ComponentModel.DataAnnotations;

public class CartItem
{
    public int CartItemID { get; set; }

    [Required]
    public int CartID { get; set; }

    [Required]
    public int ClothingID { get; set; }

    [Required]
    public int Quantity { get; set; } = 1;

    [MaxLength(20)]
    public string SelectedSize { get; set; } = null!;
    public DateTime AddedAt { get; set; }

    public Cart Cart { get; set; } = null!;
    public Clothing Clothing { get; set; } = null!;
}