using pimi_connect_app.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public Guid uuid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public Guid ProfilePictureKey { get; set; }
        public Auth Auth { get; set; }
        public Attachment ProfilePicture { get; set; }
        public List<UserChat> UserChats { get; set; }
    }
}
