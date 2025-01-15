using pimi_connect_app.Data.Entities;

namespace pimi_connect_api.User;

public interface IUserRepository
{
    Task<UserEntity?> GetByEmail(string email);
}