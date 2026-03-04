using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Data;
using TryOnVerse.API.Models;
using TryOnVerse.API.DTOs;
using TryOnVerse.API.Helpers;
using TryOnVerse.API.Common;

namespace TryOnVerse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/users/customers
    [HttpPost]
    [Route("customers")]
    public async Task<IActionResult> CreateCustomer([FromBody] RegisterCustomerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if email already exists
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return Conflict("A user with this email already exists.");

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Role = UserRoleConstants.Customer,
            PasswordHash = PasswordHasher.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
            // Navigation properties are already initialized in the User class
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Return 201 Created with location pointing to GetUserByEmail
        return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, new
        {
            user.UserID,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role,
            user.CreatedAt,
            user.UpdatedAt,
            user.IsActive
        });
    }

    // GET: api/users/id
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserID == id && u.IsActive);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.UserID,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role,
            user.CreatedAt,
            user.UpdatedAt,
            user.IsActive
        });
    }


    // TODO: include password protection so people can't just steal others' information
    // GET: api/users?email=example@example.com
    // [HttpGet]
    // public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    // {
    //     if (string.IsNullOrEmpty(email))
    //         return BadRequest("Email is required.");

    //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    //     if (user == null)
    //         return NotFound();

    //     return Ok(new
    //     {
    //         user.UserID,
    //         user.FirstName,
    //         user.LastName,
    //         user.Email,
    //         user.Role,
    //         user.CreatedAt,
    //         user.UpdatedAt
    //     });
    // }
}