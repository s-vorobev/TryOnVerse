namespace TryOnVerse.API.Data;

using System;
using System.ComponentModel.DataAnnotations;

public class Address
{
    public int AddressID { get; set; }

    [Required]
    public int UserID { get; set; }

    [Required, MaxLength(150)]
    public string Street { get; set; } = null!;

    [Required, MaxLength(100)]
    public string City { get; set; } = null!;

    [Required, MaxLength(100)]
    public string State { get; set; } = null!;

    [Required, MaxLength(20)]
    public string ZipCode { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Country { get; set; } = null!;

    [Required, MaxLength(20)]
    public string AddressType { get; set; } = null!;

    public bool IsSavedToAccount { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
}