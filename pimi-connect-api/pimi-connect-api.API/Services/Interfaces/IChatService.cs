using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services.Interfaces
{
    public interface IChatService
    {
        Task<ChatDto> GetChatAsync(Guid id);
        Task<IEnumerable<ChatDto>> GetAllChatsAsync();
        Task<ChatDto> UpdateChatAsync(ChatDto chatDto);
        Task<ChatDto> AddChatAsync(ChatDto chatDto);
        Task DeleteChatAsync(Guid id);
    }
}