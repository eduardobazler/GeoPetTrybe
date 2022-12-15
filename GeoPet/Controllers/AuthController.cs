using GeoPet.Controllers.TypesReq;
using GeoPet.Services.UserService;
using GeoPet.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoPet.Controllers;

[AllowAnonymous]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenManager _jwtTokenManager;
    private readonly IUserService _userService;
    
    public AuthController(IJwtTokenManager jwtTokenManager, IUserService userService)
    {
        _jwtTokenManager = jwtTokenManager;
        _userService = userService;
    }
    
    [HttpPost("Authenticate")]
    public IActionResult Authenticate([FromBody]AuthUser request)
    {
        var user = _userService.FindUser(request);
        if (user is null) return Unauthorized();
        
        var token = _jwtTokenManager.Authenticate(user);
        return Ok(token);
    }
    
}