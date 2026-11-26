using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace LoginApi.Services;

public class TokenService(IConfiguration config)
{
    public string Generate(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(config["Jwt:ExpiresMinutes"]!)),
            signingCredentials: creds);

        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}