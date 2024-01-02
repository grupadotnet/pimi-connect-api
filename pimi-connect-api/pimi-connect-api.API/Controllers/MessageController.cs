using Microsoft.AspNetCore.Mvc;
using pimi_connect_api.Services.Interfaces;

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
        [Route("GetAllMessagesAsync")]
        public async Task<IActionResult> GetAllMessagesAsync()
        {

            var messageDtoList = await _messageService.GetAllMessagesAsync();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(StatusCodes.Status200OK, messageDtoList);
        }


    }
}
