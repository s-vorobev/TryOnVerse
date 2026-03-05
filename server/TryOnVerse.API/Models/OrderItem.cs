namespace TryOnVerse.API.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TryOnVerse.API.Common;

public class OrderItem
{
    [Key]
    public int OrderItemID { get; set; }

    [Required]
    public int OrderID { get; set; }

    [ForeignKey(nameof(OrderID))]
    public Order Order { get; set; } = null!;

    [Required]
    public int ClothingID { get; set; }

    [ForeignKey(nameof(ClothingID))]
    public Clothing Clothing { get; set; } = null!;

    [Required]
    public int Quantity { get; set; } = 1;

    [MaxLength(DbConstants.Clothing.SizeMaxLength)]
    public string SelectedSize { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }
}