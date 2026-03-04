namespace TryOnVerse.API.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int UserID { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = "Customer";

    public DateTime? LoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public ICollection<Cart> Carts { get; set; } = null!;
    public ICollection<Address> Addresses { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;
}