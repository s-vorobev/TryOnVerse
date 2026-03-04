using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Data;

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
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: Hash the password before saving
        // user.PasswordHash = HashPassword(user.PasswordHash);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Return 201 Created with created user info
        return CreatedAtAction(nameof(GetUserByEmail), new { email = user.Email }, user);
    }

    // TODO: replace this with something like authenticate user api call that posts the password hash???
    // GET: api/users?email=sergei@example.com
    [HttpGet]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
    {
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email is required.");

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}