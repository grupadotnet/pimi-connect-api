using pimi_connect_app.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pimi_connect_app.Data.Models
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }
        //public Guid ChatId { get; set; }
        public Guid UserCreatedId { get; set; }
        public Guid AttachmentId { get; set; }
        public Guid IdPasswordContainer { get; set; }
    }
}
