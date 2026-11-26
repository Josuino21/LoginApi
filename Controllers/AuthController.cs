using LoginApi.Data;
using LoginApi.Dtos;
using LoginApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginApi.Services;

namespace LoginApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AppDbContext db, TokenService tokens) : ControllerBase
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

[HttpPost("login")]
public async Task<IActionResult> Lo0gin(LoginDto dto){
    var email = dto.Email.Trim().ToLowerInvariant();
    var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

    if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) 
        return Unauthorized(new { message = "Credenciais inválidas."});
    
    return Ok(new {token = tokens.Generate(user) });
}
}