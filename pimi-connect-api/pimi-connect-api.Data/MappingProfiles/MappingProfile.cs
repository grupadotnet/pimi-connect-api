using AutoMapper;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_app.Data.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, UserDto>().ReverseMap();
        CreateMap<UserChatEntity, UserChatDto>().ReverseMap();
        CreateMap<ChatEntity, ChatDto>().ReverseMap();
        CreateMap<MessageEntity, MessageDto>().ReverseMap();
    }
}