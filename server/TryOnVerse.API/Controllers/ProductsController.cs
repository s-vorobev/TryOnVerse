using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryOnVerse.API.Data;
using TryOnVerse.API.Models;

namespace TryOnVerse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }
}