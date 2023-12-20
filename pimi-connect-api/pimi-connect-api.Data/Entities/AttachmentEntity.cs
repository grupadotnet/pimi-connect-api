namespace pimi_connect_app.Data.Entities;

public class AttachmentEntity
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Path { get; set; }
    public string TableName { get; set; }
    public Guid ObjectId { get; set; }
}