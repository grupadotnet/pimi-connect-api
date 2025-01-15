namespace pimi_connect_api.Message;

public interface IMessageRepository
{
    Task<bool> MessageExists(Guid id);
}