using pimi_connect_app.Data.Enums;

namespace pimi_connect_app.Data.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserStatus Status { get; set; }
    public Guid ProfilePictureId { get; set; }
    public AttachmentDto? ProfilePicture { get; set; }
}