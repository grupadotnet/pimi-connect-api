namespace pimi_connect_app.Data.Entities
{
    public class UserKeyEntity
    {
        public Guid Id { get; set; }
        public byte[] IndirectKey { get; set; }
    }
}
