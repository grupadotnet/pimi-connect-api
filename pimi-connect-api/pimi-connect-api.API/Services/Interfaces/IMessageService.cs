using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services.Interfaces
{
    public interface IMessageService
    {
        
        Task<IEnumerable<MessageDto>> GetAllMessagesAsync();
        Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync();
        Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync();
        Task<MessageDto> GetMessageAsync(Guid messageId);
        Task<MessageDto> AddMessageAsync(MessageDto messageDto);
        Task DeleteMessageAsync(Guid messageId);
    }
}
