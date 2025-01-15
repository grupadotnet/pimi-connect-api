using Microsoft.AspNetCore.Mvc;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.UserChat;

[ApiController]
[Route("api/[controller]")]
public class UserChatController : ControllerBase
{
    private readonly IUserChatService _userChatService;

    public UserChatController(IUserChatService userChatService)
    {
        _userChatService = userChatService;
    }
    
    [HttpGet]
    [Route("GetAsync")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var userChatDto = await _userChatService.GetUserChatAsync(id);
        
        return StatusCode(StatusCodes.Status200OK, userChatDto);
    }
    
    [HttpGet]
    [Route("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var userDtoList = await _userChatService.GetAllUserChatsAsync();
        
        return StatusCode(StatusCodes.Status200OK, userDtoList);
    }
    
    [HttpPut]
    [Route("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync(UserChatDto userChatDto)
    {
        var updatedUserChatDto = await _userChatService.UpdateUserChatAsync(userChatDto);
        
        return StatusCode(StatusCodes.Status200OK, updatedUserChatDto);
    }
    
    [HttpPost]
    [Route("AddAsync")]
    public async Task<IActionResult> AddAsync(UserChatDto userChatDto)
    {
        var addedUserChatDto = await _userChatService.AddUserChatAsync(userChatDto);
        
        return StatusCode(StatusCodes.Status200OK, addedUserChatDto);
    }
    
    [HttpDelete]
    [Route("DeleteAsync")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _userChatService.DeleteUserChatAsync(id);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }
}