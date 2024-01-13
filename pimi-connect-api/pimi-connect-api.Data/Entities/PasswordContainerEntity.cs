namespace pimi_connect_app.Data.Entities
{
    public class PasswordContainerEntity
    {
        public Guid Id {  get; set; }
        public Guid ChatId { get; set; }
        //public List<ChatPasswordEntity> ChatPassword { get; set; } // Unable to determine the relationship
    }
}
