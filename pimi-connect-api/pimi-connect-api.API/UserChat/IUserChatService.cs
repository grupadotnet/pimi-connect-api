using pimi_connect_app.Data.Models;

namespace pimi_connect_api.UserChat;

public interface IUserChatService
{
    Task<UserChatDto> GetUserChatAsync(Guid id);
    Task<IEnumerable<UserChatDto>> GetAllUserChatsAsync();
    Task<UserChatDto> UpdateUserChatAsync(UserChatDto userChatDto);
    Task<UserChatDto> AddUserChatAsync(UserChatDto userChatDto);
    Task DeleteUserChatAsync(Guid id);
}