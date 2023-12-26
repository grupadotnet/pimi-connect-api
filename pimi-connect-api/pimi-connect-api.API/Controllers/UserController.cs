using Microsoft.AspNetCore.Mvc;
using pimi_connect_api.Services.Interface;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [Route("GetAsync")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var userDto = await _userService.GetUserAsync(id);
        
        return StatusCode(StatusCodes.Status200OK, userDto);
    }
    
    [HttpGet]
    [Route("GetByEmailAsync")]
    public async Task<IActionResult> GetByEmailAsync(string email)
    {
        var userDto = await _userService.GetUserByEmailAsync(email);

        return StatusCode(StatusCodes.Status200OK, userDto);
    }
    
    [HttpGet]
    [Route("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var userDtoList = await _userService.GetAllUsersAsync();
        
        return StatusCode(StatusCodes.Status200OK, userDtoList);
    }
    
    [HttpPut]
    [Route("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync(UserDto userDto)
    {
        var updatedUserDto = await _userService.UpdateUserAsync(userDto);
        
        return StatusCode(StatusCodes.Status200OK, updatedUserDto);
    }
    
    [HttpPost]
    [Route("AddAsync")]
    public async Task<IActionResult> AddAsync(UserDto userDto)
    {
        var addedUserDto = await _userService.AddUserAsync(userDto);
        
        return StatusCode(StatusCodes.Status200OK, addedUserDto);
    }
    
    [HttpDelete]
    [Route("DeleteAsync")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }
}