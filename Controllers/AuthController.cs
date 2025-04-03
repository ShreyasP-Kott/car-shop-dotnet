using Microsoft.AspNetCore.Mvc;
using MongoConnection.Auth;
using MongoConnection.Services;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { Message = "Email and password are required" });
        }

        var response = await _authService.Authenticate(request.Email, request.Password);
        return response.Token == "" ? Unauthorized(response) : Ok(response);
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
