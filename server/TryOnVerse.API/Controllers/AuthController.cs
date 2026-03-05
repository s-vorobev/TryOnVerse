using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Data;
using TryOnVerse.API.Helpers;
using TryOnVerse.API.Models;
using TryOnVerse.API.DTOs;

namespace TryOnVerse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, JwtService jwtService, IConfiguration config)
    {
        _context = context;
        _jwtService = jwtService;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        // Update last login timestamp
        user.LoginAt = DateTime.UtcNow;

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();
        var jwtSettings = _config.GetSection("JwtSettings");

        // Revoke old refresh tokens
        foreach (var token in user.RefreshTokens)
            token.IsRevoked = true;

        // Add new refresh token
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(int.Parse(jwtSettings["RefreshTokenDays"]!)),
            IsRevoked = false
        });

        await _context.SaveChangesAsync();

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            user.UserID,
            user.Email,
            user.Role,
            user.LoginAt
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshDto dto)
    {
        if (string.IsNullOrEmpty(dto.RefreshToken))
            return BadRequest("Refresh token is required");

        // Find the token in DB
        var token = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

        if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
            return Unauthorized("Invalid or expired refresh token");

        // Optionally revoke the old token (rotate)
        token.IsRevoked = true;

        // Generate new tokens
        var accessToken = _jwtService.GenerateAccessToken(token.User);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        var jwtSettings = _config.GetSection("JwtSettings");
        token.User.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(int.Parse(jwtSettings["RefreshTokenDays"]!)),
            IsRevoked = false
        });

        await _context.SaveChangesAsync();

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshDto dto)
    {
        if (string.IsNullOrEmpty(dto.RefreshToken))
            return BadRequest("Refresh token is required");

        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

        if (token == null || token.IsRevoked)
            return NotFound("Refresh token not found or already revoked");

        token.IsRevoked = true;
        await _context.SaveChangesAsync();

        return NoContent(); // 204
    }
}