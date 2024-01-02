using pimi_connect_app.Data.Enums;

namespace pimi_connect_app.Data.Entities
{
    public class UserChatEntity
    {
        public string NickName { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public Guid? LastReadMessageId { get; set; }
        public Guid UserKeyId { get; set; }
        public ChatRole Role { get; set; }
        public MessageEntity? LastReadMessage { get; set; }
        public UserEntity? User { get; set; }
        public ChatEntity? Chat { get; set; }

    }
}
