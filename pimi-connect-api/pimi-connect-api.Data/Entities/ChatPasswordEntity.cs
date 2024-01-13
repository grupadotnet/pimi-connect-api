namespace pimi_connect_app.Data.Entities
{
    public class ChatPasswordEntity
    {
        public Guid PasswordContainerId { get; set; }
        public Guid UserId {  get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
