using Microsoft.AspNetCore.Mvc;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var chatDtoList = await _chatService.GetAllChatsAsync();
            return StatusCode(StatusCodes.Status200OK, chatDtoList);
        }
        [HttpGet]
        [Route("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var chatDto = await _chatService.GetChatAsync(id);
            return StatusCode(StatusCodes.Status200OK, chatDto);
        }
        [HttpPost]
        [Route("AddAsync")]
        public async Task<IActionResult> AddAsync(ChatDto chatDto)
        {
            var addedChatDto = await _chatService.AddChatAsync(chatDto);
            return StatusCode(StatusCodes.Status200OK, addedChatDto);
        }
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ChatDto chatDto)
        {
            var updatedChatDto = await _chatService.UpdateChatAsync(chatDto);
            return StatusCode(StatusCodes.Status200OK, updatedChatDto);
        }
        [HttpDelete]
        [Route("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _chatService.DeleteChatAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
