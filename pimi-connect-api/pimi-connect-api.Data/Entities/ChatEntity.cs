﻿namespace pimi_connect_app.Data.Entities
{
    public class ChatEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ThumbnailId { get; set; }
        public AttachmentEntity? Thumbnail { get; set; }
        public int p { get; set; } // prop name to change 
        public int g { get; set; } // prop name to change
        public List<MessageEntity>?  Messages { get; set; }
        //public List<UserChatEntity>? Users { get; set; } //Unable to determine the relationship represented by navigation 'ChatEntity.Users' of type 'List<UserChatEntity>'
        
    }
}
