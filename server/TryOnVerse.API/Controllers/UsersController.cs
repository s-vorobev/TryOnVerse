using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TryOnVerse.API.Data;
using TryOnVerse.API.Models;
using TryOnVerse.API.DTOs;
using TryOnVerse.API.Helpers;
using TryOnVerse.API.Common;
using System.IdentityModel.Tokens.Jwt;

namespace TryOnVerse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication for all actions
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/users/customers
    [AllowAnonymous] // Allow registration without being logged in
    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomer([FromBody] RegisterCustomerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

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
        // Get the user ID from the JWT token
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (!int.TryParse(userIdClaim, out var currentUserId))
            return Unauthorized();

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserID == id && u.IsActive);

        if (user == null)
            return NotFound();

        // Only allow the user to access their own info or if they are an admin
        var userRole = User.FindFirstValue(ClaimTypes.Role);
        if (user.UserID != currentUserId && userRole != UserRoleConstants.Admin)
            return Forbid();

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
}