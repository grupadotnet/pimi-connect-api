using Microsoft.AspNetCore.Mvc;
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
        [Route("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var messageDto = await _messageService.GetMessageAsync(id);
            return StatusCode(StatusCodes.Status200OK, messageDto);
        }

    }
}
