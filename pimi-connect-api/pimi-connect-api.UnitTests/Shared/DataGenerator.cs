using AutoMapper;

namespace pimi_connect_api.UnitTests.Shared;

public static class DataGenerator
{
    #region Generate User related data
    public static string GenerateUserName(Guid id = new ())
    {
        return $"user{id}";
    }

    public static string GenerateEmail(string domain, Guid id = new())
    { 

        return $"user{id}@{domain}";
    }

    public static UserEntity GenerateUserEntity(string domain, Guid id = new ())
    {
        return new UserEntity()
        {
            Id = id,
            UserName = GenerateUserName(id),
            Email = GenerateEmail(domain,id),
            Status = UserStatus.Accessible,
            ProfilePictureId = id
        };
    }
    
    public static UserDto GenerateUserDto(IMapper mapper, string domain, Guid id = new ())
    {
        var userEntity = GenerateUserEntity(domain, id);

        return mapper.Map<UserDto>(userEntity);
    }
    #endregion
    
    #region Generate Attachment related data
    public static AttachmentEntity GenerateAttachmentEntity(Guid id = new ())
    {
        return new AttachmentEntity
        {
            Id = id,
            Type = AttachmentType.ProfilePicture,
            Extension = ".jpg",
            Path = "/some/path",
            publicName = "publicName"
        };
    }
    
    public static AttachmentDto GenerateAttachmentDto(IMapper mapper, Guid id = new ())
    {
        var attachmentEntity = GenerateAttachmentEntity(id);

        return mapper.Map<AttachmentDto>(attachmentEntity);
    }
    #endregion
}