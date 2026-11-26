using System.ComponentModel.DataAnnotations;

namespace LoginApi.Dtos;

public record RegisterDto(
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password
);
public record LoginDto(
    [Required, EmailAddress] string Email,
    [Required] string Password
);