namespace pimi_connect_app.Data.Entities
{
    public class MessageEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ChatId { get; set; }
        public Guid UserCreatedId {  get; set; }
        public Guid AttachmentId { get; set; }
        public Guid IdPasswordContainer { get; set; }
        public UserEntity UserCreated { get; set; }
        public ChatEntity Chat { get; set; }
        public List<AttachmentEntity>? Attachments { get; set; }
    }
}
