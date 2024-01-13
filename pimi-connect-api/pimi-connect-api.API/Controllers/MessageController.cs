using Microsoft.AspNetCore.Mvc;
using pimi_connect_api.Services;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var messageDtoList = await _messageService.GetAllMessagesAsync();
            return StatusCode(StatusCodes.Status200OK, messageDtoList);
        }
        [HttpGet]
        [Route("GetByUserIdAsync")]
        public async Task<IActionResult> GetByUserIdAsync(Guid id)
        {
            var messageDtoList = await _messageService.GetMessagesByUserIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, messageDtoList);
        }
        [HttpGet]
        [Route("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var messageDto = await _messageService.GetMessageAsync(id);
            return StatusCode(StatusCodes.Status200OK, messageDto);
        }
        [HttpPost]
        [Route("AddAsync")]
        public async Task<IActionResult> AddAsync(MessageDto messageDto)
        {
            var addedMessageDto = await _messageService.AddMessageAsync(messageDto);

            return StatusCode(StatusCodes.Status200OK, addedMessageDto);
        }
        [HttpDelete]
        [Route("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _messageService.DeleteMessageAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
