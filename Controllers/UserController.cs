using Microsoft.AspNetCore.Mvc;
using MongoConnection.Services; 
using MongoConnection.Model;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] AllDetails request)
    {
        var user = new UserDetails { Name = request.Name, Email = request.Email, Password = request.Password };
        var address = new Address { Street = request.Street, City = request.City, State = request.State, ZipCode = request.ZipCode };

        var userId = await _userService.CreateUserWithAddress(user, address);
        return Ok(new { Message = "User created successfully", UserId = userId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserWithAddress(id);
        if (user == null)
            return NotFound("User not found");

        return Ok(user);
    }
}
