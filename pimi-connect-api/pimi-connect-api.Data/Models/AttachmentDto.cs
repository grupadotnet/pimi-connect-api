using pimi_connect_app.Data.Enums;

namespace pimi_connect_app.Data.Models;

public class AttachmentDto
{
    public Guid Id { get; set; }
    public AttachmentType Type { get; set; }
    public string Extension { get; set; }
    public string Path { get; set; }
    public string PublicName { get; set; }
}