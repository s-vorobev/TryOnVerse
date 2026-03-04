namespace TryOnVerse.API.Models;

using System.ComponentModel.DataAnnotations;

public class OrderItem
{
    public int OrderItemID { get; set; }

    [Required]
    public int OrderID { get; set; }

    [Required]
    public int ClothingID { get; set; }

    [Required]
    public int Quantity { get; set; } = 1;

    [MaxLength(20)]
    public string SelectedSize { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    public Order Order { get; set; } = null!;
    public Clothing Clothing { get; set; } = null!;
}