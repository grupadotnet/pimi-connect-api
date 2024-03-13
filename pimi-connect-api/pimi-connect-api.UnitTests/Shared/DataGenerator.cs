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
            ProfilePictureId = id
        };
    }
    
    public static UserDto GenerateUserDto(IMapper mapper, Guid id = new ())
    {
        var userEntity = GenerateUserEntity(id);

        return mapper.Map<UserDto>(userEntity);
    }

    public static ChatEntity GenerateChatEntity(Guid id = new (), Guid thumbnailID = new())
    {
        return new ChatEntity()
        {
            Id = id,
            Name = $"chat{id}",
            ThumbnailId = thumbnailID,
            Thumbnail = GenerateAttachmentEntity(thumbnailID)
            
        };
    }

    public static ChatDto GenerateChatDto(IMapper mapper, Guid id = new (), Guid thumbnailID = new())
    {
        var chatEntity = GenerateChatEntity(id);

        return mapper.Map<ChatDto>(chatEntity);
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