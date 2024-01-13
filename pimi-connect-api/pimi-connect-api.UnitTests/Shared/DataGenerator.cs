using AutoMapper;

namespace pimi_connect_api.UnitTests.Shared;

public static class DataGenerator
{
    #region Generate User related data

    public static string GenerateUserName(Guid id = new ())
    {
        return $"user{id}";
    }
    
    public static string GenerateEmail(Guid id = new ())
    {
        return $"user{id}@domain.com";
    }
    
    public static UserEntity GenerateUserEntity(Guid id = new ())
    {
        return new UserEntity()
        {
            Id = id,
            UserName = GenerateUserName(id),
            Email = GenerateEmail(id),
            Status = UserStatus.Accessible,
            ProfilePictureId = Guid.NewGuid()
        };
    }
    
    public static UserDto GenerateUserDto(IMapper mapper, Guid id = new ())
    {
        var userEntity = GenerateUserEntity(id);

        return mapper.Map<UserDto>(userEntity);
    }

    #endregion
    
    
}