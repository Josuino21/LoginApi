using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.Controllers;

[ApiController]
[Route("api/me")]
[Authorize]
public class MeController: ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub"),
        email = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue("email")});
}
