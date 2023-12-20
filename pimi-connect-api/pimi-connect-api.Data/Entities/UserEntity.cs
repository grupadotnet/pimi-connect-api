using pimi_connect_app.Data.Models;
using pimi_connect_app.Data.Entities;

namespace Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
        public Guid ProfilePictureKey { get; set; }
        public AuthEntity Auth { get; set; }
        public AttachmentEntity ProfilePicture { get; set; }
        public List<UserChatEntity> UserChats { get; set; }
    }
}
