using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services.Interfaces
{
    public interface IMessageService
    {
        
        Task<IEnumerable<MessageDto>> GetAllMessagesAsync();
        Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync();
        Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync();
        Task<MessageDto> GetMessageAsync(int messageId);
        Task<MessageDto> AddMessageAsync(MessageDto messageDto);
        bool MessageExistsAsync(int messageId);
        bool Save();
    }
}
