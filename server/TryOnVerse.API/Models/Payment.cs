namespace TryOnVerse.API.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TryOnVerse.API.Common;

public class Payment
{
    [Key]
    public int PaymentID { get; set; }

    [Required]
    public int OrderID { get; set; }

    [ForeignKey(nameof(OrderID))]
    public Order Order { get; set; } = null!;

    [Required]
    public decimal Amount { get; set; }

    [Required, MaxLength(DbConstants.Payment.PaymentMethodMaxLength)]
    public string PaymentMethod { get; set; } = null!;

    [Required, MaxLength(DbConstants.Payment.PaymentStatusMaxLength)]
    public string PaymentStatus { get; set; } = "Pending";

    [MaxLength(DbConstants.Payment.TransactionReferenceMaxLength)]
    public string TransactionReference { get; set; } = null!;

    public DateTime? PaidAt { get; set; }
}