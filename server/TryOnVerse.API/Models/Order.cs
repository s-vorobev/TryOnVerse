namespace TryOnVerse.API.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Order
{
    public int OrderID { get; set; }

    [Required]
    public int UserID { get; set; }

    [Required]
    public int AddressID { get; set; }

    public DateTime OrderDate { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [Required, MaxLength(30)]
    public string Status { get; set; } = "Pending";

    public User User { get; set; } = null!;
    public Address Address { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Payment Payment { get; set; } = null!;
}