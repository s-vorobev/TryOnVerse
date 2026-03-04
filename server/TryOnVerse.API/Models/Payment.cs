namespace TryOnVerse.API.Data;

using System;
using System.ComponentModel.DataAnnotations;

public class Payment
{
    public int PaymentID { get; set; }

    [Required]
    public int OrderID { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required, MaxLength(30)]
    public string PaymentMethod { get; set; } = null!;

    [Required, MaxLength(30)]
    public string PaymentStatus { get; set; } = "Pending";

    [MaxLength(100)]
    public string TransactionReference { get; set; } = null!;

    public DateTime? PaidAt { get; set; }

    public Order Order { get; set; } = null!;
}