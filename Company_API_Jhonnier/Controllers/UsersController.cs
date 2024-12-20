using Company_API_Jhonnier.Interfaces;
using Company_API_Jhonnier.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company_API_Jhonnier.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(AddUser user)
    {
        var result = await _userService.CreateUser(user);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUser user)
    {
        var result = await _userService.UpdateUser(user);
        return Ok(result);
    }
}