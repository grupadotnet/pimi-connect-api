using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Enums;

namespace pimi_connect_app.Data.Models;

public class UserChatDto
{
    public string NickName { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public Guid LastReadMessageId { get; set; }
    public Guid UserKeyId { get; set; }
    public ChatRole Role { get; set; }
    public MessageDto? LastReadMessage { get; set; }
    public UserDto? User { get; set; }
    public ChatDto? Chat { get; set; }
}