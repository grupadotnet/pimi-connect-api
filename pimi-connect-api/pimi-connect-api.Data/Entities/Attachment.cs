namespace pimi_connect_app.Data.Entities;

abstract public class Attachment
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Path { get; set; }
    public string publicName { get; set; }
}