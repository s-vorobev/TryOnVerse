using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Data;
using TryOnVerse.API.Models;
using TryOnVerse.API.DTOs;
using TryOnVerse.API.Helpers;

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

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto dto)
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
            Role = dto.Role,
            PasswordHash = PasswordHasher.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
            // Navigation properties are already initialized in the User class
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Return 201 Created with location pointing to GetUserByEmail
        return CreatedAtAction(nameof(GetUserByEmail), new { email = user.Email }, new
        {
            user.UserID,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role,
            user.CreatedAt,
            user.UpdatedAt
        });
    }

    // GET: api/users?email=example@example.com
    [HttpGet]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    {
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email is required.");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

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
            user.UpdatedAt
        });
    }
}