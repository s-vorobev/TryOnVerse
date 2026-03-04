namespace TryOnVerse.API.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TryOnVerse.API.Common;

public class Address
{
    public int AddressID { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey(nameof(UserID))]
    public User User { get; set; } = null!;

    [Required, MaxLength(DbConstants.Address.StreetMaxLength)]
    public string Street { get; set; } = null!;

    [Required, MaxLength(DbConstants.Address.CityMaxLength)]
    public string City { get; set; } = null!;

    [Required, MaxLength(DbConstants.Address.StateMaxLength)]
    public string State { get; set; } = null!;

    [Required, MaxLength(DbConstants.Address.ZipCodeMaxLength)]
    public string ZipCode { get; set; } = null!;

    [Required, MaxLength(DbConstants.Address.CountryMaxLength)]
    public string Country { get; set; } = null!;

    [Required, MaxLength(DbConstants.Address.AddressTypeMaxLength)]
    public string AddressType { get; set; } = null!;

    public bool IsSavedToAccount { get; set; } = false;

    public DateTime CreatedAt { get; set; }
}