using LoginApi.Data;
using LoginApi.Dtos;
using LoginApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        if (await db.Users.AnyAsync(u => u.Email == email))
            return Conflict(new { message = "E-mail já cadastrado." });

        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return Created("", new { user.Id, user.Email });
    }
}