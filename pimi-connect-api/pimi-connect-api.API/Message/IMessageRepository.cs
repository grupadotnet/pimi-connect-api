using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Repository;

namespace pimi_connect_api.Message;

public interface IMessageRepository : IRepository<MessageEntity>
{
    Task<bool> MessageExists(Guid id);
}