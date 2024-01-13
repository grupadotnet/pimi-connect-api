using pimi_connect_app.Data.Enums;

namespace pimi_connect_app.Data.Entities
{
    public class EmailEntity
    {
        public Guid Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public EmailTemplate Template { get; set; }
        public DateTime SentAt { get; set; }
    }
}
