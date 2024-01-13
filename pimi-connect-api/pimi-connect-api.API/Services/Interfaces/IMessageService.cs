using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services.Interfaces
{
    public interface IMessageService
    {
        
        Task<IEnumerable<MessageDto>> GetAllMessagesAsync();
        Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync(Guid userId);
        Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(Guid chatId);
        Task<MessageDto> GetMessageAsync(Guid messageId);
        Task<MessageDto> AddMessageAsync(MessageDto messageDto);
        Task DeleteMessageAsync(Guid messageId);
    }
}
