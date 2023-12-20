using System.Net.Mail;
using Models;

namespace pimi_connect_app.Data.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserStatusEnum Status { get; set; }
    public Guid ProfilePictureKey { get; set; }
    public AttachmentDto ProfilePicture { get; set; }
}