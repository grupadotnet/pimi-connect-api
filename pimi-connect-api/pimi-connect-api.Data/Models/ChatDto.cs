using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pimi_connect_app.Data.Models
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ThumbnailKey { get; set; }
        public AttachmentDto? Thumbnail { get; set; }

    }
}
