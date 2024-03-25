using AutoMapper;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace pimi_connect_api.UnitTests.Shared;

public static class DataGenerator
{
    #region Generate User related data
    private static byte[] GenerateRandomBytes(int length)
    {
        var random = new Random();
        byte[] buffer = new byte[length];
        random.NextBytes(buffer);
        return buffer;
    }

    public static string GenerateUserName(Guid id = new ())
    {
        return $"user{id}";
    }

    public static string GenerateEmail(string domain, Guid id = new())
    { 

        return $"user{id}@{domain}";
    }

    public static byte[] GeneratePassword(byte[] salt)
    {
        Random r = new Random();
        int lenght = r.Next(8, 16);
        string password = string.Empty;
        for (int i = 0; i < lenght; i++)
        {
            password += (char)r.Next(33, 126);
        }

        byte[] passwordHashed;

        using(SHA256 sha256 = SHA256.Create())
        {
            passwordHashed = sha256.ComputeHash(Encoding.ASCII.GetBytes(password + Convert.ToBase64String(salt)));
        }

        return passwordHashed;
    }

    public static AuthEntity GenerateAuthEntity(Guid id = new ())
    {
        byte[] salt = GenerateRandomBytes(16);
        return new AuthEntity()
        {
            Id = id,
            PasswordSalt = salt,
            PasswordHash = GeneratePassword(salt),
            PrivateKey = GenerateRandomBytes(32)
        };
    }

    public static UserEntity GenerateUserEntity(string domain, Guid id = new ())
    {
        return new UserEntity()
        {
            Id = id,
            UserName = GenerateUserName(id),
            Email = GenerateEmail(domain,id),
            Status = UserStatus.Accessible,
            ProfilePictureId = id,
            Auth = GenerateAuthEntity(id),
            ProfilePicture = GenerateAttachmentEntity(id)
            //UserChats
            //Messages
            
        };
    }
    
    public static UserDto GenerateUserDto(IMapper mapper, string domain, Guid id = new ())
    {
        var userEntity = GenerateUserEntity(domain, id);

        return mapper.Map<UserDto>(userEntity);
    }
    #endregion
    
    #region Generate Attachment related data

    private static string GenerateExtension()
    {
        var r = new Random();
        List<string> mediaFileExtensions = new List<string>
        {
            ".mp3",
            ".wav",
            ".flac",
            ".aac",
            ".ogg",
            ".wma",
            ".m4a",
            ".mp4",
            ".avi",
            ".mkv",
            ".mov",
            ".wmv",
            ".flv",
            ".webm",
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".bmp",
            ".tiff",
            ".svg",
            ".mpg",
            ".mpeg",
            ".mpe",
            ".mp2",
            ".mpv",
            ".m4v",
            ".ogg",
            ".oga",
            ".ogv",
            ".opus",
            ".ico",
            ".pdf",
            ".doc",
            ".docx",
            ".xls",
            ".xlsx",
            ".ppt",
            ".pptx",
            // Add more extensions as needed
        };

        return mediaFileExtensions[r.Next(mediaFileExtensions.Count)];
    }

    private static Tuple<string, string> GenerateFileNameAndPath(Guid id = new())
    {
        Dictionary<string, string> fileTypePrefixes = new Dictionary<string, string>
        {
            { "Songs", "Song" },
            { "Videos", "Video" },
            { "Documents", "Doc" },
            { "Images", "Image" },
            { "Clips", "Clip" },
            { "Media", "Media" },
            { "Audio", "Audio" },
            { "Files", "File"}
        };

        var r = new Random();
        int randomNumber = r.Next(fileTypePrefixes.Count);
        string fileName = $"{fileTypePrefixes.Values.ElementAt(randomNumber)}{id}";
        string path = $"/{fileTypePrefixes.Keys.ElementAt(randomNumber)}/{fileName}";
        return new Tuple<string,string>(fileName, path);
    }
    public static AttachmentEntity GenerateAttachmentEntity(Guid id = new ())
    {
        var fileNameAndPath = GenerateFileNameAndPath(id);

        return new AttachmentEntity
        {
            Id = id,
            Type = AttachmentType.ProfilePicture,
            Extension = GenerateExtension(),
            Path = fileNameAndPath.Item2,
            publicName = fileNameAndPath.Item1
        };
    }
    
    public static AttachmentDto GenerateAttachmentDto(IMapper mapper, Guid id = new ())
    {
        var attachmentEntity = GenerateAttachmentEntity(id);

        return mapper.Map<AttachmentDto>(attachmentEntity);
    }
    #endregion

    #region Generate UserChat related data

    public static UserChatEntity GenerateUserChatEntity(UserEntity user, Guid id = new ())
    {
        var chatID = Guid.NewGuid();
        var lastmessageID = Guid.NewGuid();
        var userKeyID = Guid.NewGuid();
        return new UserChatEntity
        {
            Id = id,
            UserId = user.Id,
            ChatId = chatID,
            LastReadMessageId = lastmessageID,
            NickName = GenerateNickName(id),
            //LastReadMessage = GenerateMessageEntity(lastmessageID),
            Role = ChatRole.Admin,
            User = user,
            //Chat = GenerateChatEntity(chatID),
            UserKeyId = userKeyID

        };
    }

    public static UserChatDto GenerateUserChatDto(IMapper mapper, UserEntity user, Guid id = new ())
    {
        var userChatEntity = GenerateUserChatEntity(user, id);

        return mapper.Map<UserChatDto>(userChatEntity);
    }

    private static string GenerateNickName(Guid id = new())
    {
        return $"nick{id}";
    }

    #endregion

    #region Generate Message related data

    public static MessageEntity GenerateMessageEntity(UserChatEntity userChat, Guid id = new ())
    {
        var userCreatedID = Guid.NewGuid();
        var attachmentID = Guid.NewGuid();
        var passwordContainerID = Guid.NewGuid();
        var attachment = GenerateAttachmentEntity(attachmentID);
        return new MessageEntity
        {
            Id = id,
            Content = $"content{id}",
            CreatedDate = DateTime.Now,
            ChatId = userChat.ChatId,
            UserCreatedId = userChat.UserId,
            AttachmentId = attachmentID,
            PasswordContainerId = passwordContainerID,
            //Chat = GenerateChatEntity(chatID),
            UserCreated = GenerateUserEntity("gmail.com", userCreatedID),
            Attachments = new List<AttachmentEntity> { attachment },
            PasswordContainer = GeneratePasswordContainerEntity(userChat.ChatId,passwordContainerID)
        };
    }

    public static MessageDto GenerateMessageDto(IMapper mapper, UserChatEntity userChat, Guid id = new ())
    {
        var messageEntity = GenerateMessageEntity(userChat, id);

        return mapper.Map<MessageDto>(messageEntity);
    }

    #endregion

    #region Generate PasswordContainer related data

    public static PasswordContainerEntity GeneratePasswordContainerEntity(Guid chatID,Guid id = new ())
    {
        return new PasswordContainerEntity
        {
            Id = id,
            ChatId = chatID,
            //ChatPassword = GenerateChatPassword()
        };
    }

    #endregion
}