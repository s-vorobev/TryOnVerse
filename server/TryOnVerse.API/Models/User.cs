namespace TryOnVerse.API.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    [JsonIgnore] // Never expose the hash in JSON
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = "Customer";

    public DateTime? LoginAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}