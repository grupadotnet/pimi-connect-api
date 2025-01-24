using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Repository;

namespace pimi_connect_api.User;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity?> GetByEmail(string email);
}