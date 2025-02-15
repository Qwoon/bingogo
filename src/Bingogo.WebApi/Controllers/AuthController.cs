using Microsoft.AspNetCore.Authorization;

namespace Bingogo.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController
{
    public AuthController()
    {

    }
}
