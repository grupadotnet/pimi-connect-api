namespace pimi_connect_app.Data.Entities
{
    public class AuthEntity
    {
        public Guid Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
