using pimi_connect_app.Data.Models;

namespace pimi_connect_api.User;

public interface IUserService
{
    Task<UserDto> GetUserAsync(Guid id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> UpdateUserAsync(UserDto userDto);
    Task<UserDto> AddUserAsync(UserDto userDto);
    Task DeleteUserAsync(Guid id);
}