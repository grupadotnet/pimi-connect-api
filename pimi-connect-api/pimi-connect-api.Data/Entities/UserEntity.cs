using pimi_connect_app.Data.Enums;
using pimi_connect_app.Data.Models;

namespace pimi_connect_app.Data.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public Guid ProfilePictureKey { get; set; }
        public AuthEntity? Auth { get; set; }
        public AttachmentEntity? ProfilePicture { get; set; }
        //public List<UserChatEntity>? UserChats { get; set; } // Unable to determine the relationship represented by navigation 'UserEntity.UserChats' of type 'List<UserChatEntity>'
    }
}
