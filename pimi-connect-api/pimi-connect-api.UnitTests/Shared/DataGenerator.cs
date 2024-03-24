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
    #endregion

    #region Generate Message related data

    public static string GenerateMessageContent(Guid id = new())
    {
        return $"messageContent{id}";
    }

    public static DateTime GenerateMessageCreatedDate()
    {
        return DateTime.Now;
    }

    public static MessageEntity GenerateMessageEntity(Guid id = new())
    {
        return new MessageEntity()
        {
            Id = id,
            Content = GenerateMessageContent(id),
            CreatedDate = GenerateMessageCreatedDate(),
            IsDeleted = false,
            UserCreatedId = Guid.NewGuid(),
            AttachmentId = Guid.NewGuid(),
            PasswordContainerId = Guid.NewGuid()
        };
    }

    public static MessageDto GenerateMessageDto(IMapper mapper, Guid id = new())
    {
        var messageEntity = GenerateMessageEntity(id);

        return mapper.Map<MessageDto>(messageEntity);
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
            PublicName = "publicName"
        };
    }
    
    public static AttachmentDto GenerateAttachmentDto(IMapper mapper, Guid id = new ())
    {
        var attachmentEntity = GenerateAttachmentEntity(id);

        return mapper.Map<AttachmentDto>(attachmentEntity);
    }
    #endregion

}