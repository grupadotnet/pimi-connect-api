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
}