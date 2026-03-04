namespace TryOnVerse.API.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TryOnVerse.API.Common;

public class Order
{
    public int OrderID { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey(nameof(UserID))]
    public User User { get; set; } = null!;

    [Required]
    public int AddressID { get; set; }

    [ForeignKey(nameof(AddressID))]
    public Address Address { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [Required, MaxLength(DbConstants.Order.StatusMaxLength)]
    public string Status { get; set; } = "Pending";

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}