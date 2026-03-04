namespace TryOnVerse.API.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TryOnVerse.API.Common;

public class CartItem
{
    public int CartItemID { get; set; }

    [Required]
    public int CartID { get; set; }

    [ForeignKey(nameof(CartID))]
    public Cart Cart { get; set; } = null!;

    [Required]
    public int ClothingID { get; set; }

    [ForeignKey(nameof(ClothingID))]
    public Clothing Clothing { get; set; } = null!;

    [Required]
    public int Quantity { get; set; } = 1;

    [MaxLength(DbConstants.Clothing.SizeMaxLength)]
    public string SelectedSize { get; set; } = null!;
    public DateTime AddedAt { get; set; }
}