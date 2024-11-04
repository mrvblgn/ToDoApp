using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Service.Abstracts;

namespace ToDoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService _userService) : ControllerBase
{
    [HttpGet("email")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByEmail([FromQuery]string email)
    {
        var result = await _userService.GetByEmailAsync(email);
        return Ok(result);
    }
}