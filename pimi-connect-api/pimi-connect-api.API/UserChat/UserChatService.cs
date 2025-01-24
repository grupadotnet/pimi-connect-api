using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.UserChat;

public class UserChatService : IUserChatService
{
    private readonly IMapper _mapper;
    private readonly IUserChatRepository _repository;

    public UserChatService(IMapper mapper, IUserChatRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<UserChatDto> GetUserChatAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var userChatEntity = await _repository.GetById(id);

        if (userChatEntity == null)
        {
            throw new NotFound404Exception("UserChat", id.ToString());
        }
        #endregion

        var userChaDto = _mapper.Map<UserChatDto>(userChatEntity);
        return userChaDto;
    }

    public async Task<IEnumerable<UserChatDto>> GetAllUserChatsAsync()
    {
        var userChatEntities = await _repository.GetAll();
        
        var userChatDtoList = _mapper.Map<List<UserChatDto>>(userChatEntities);
        return userChatDtoList;
    }

    public async Task<UserChatDto> AddUserChatAsync(UserChatDto userChatDto)
    {
        #region Check and reject if already exists
        var userChatEntity = await _repository.GetById(userChatDto.ChatId);

        if (userChatEntity != null)
        {
            throw new BadRequest400Exception(
                $"UserChat with id {userChatDto.ChatId} already exists.");
        }
        #endregion
        
        var userChatEntityToAdd = _mapper.Map<UserChatEntity>(userChatDto);

        var addedUserChatEntity = await _repository.Add(userChatEntityToAdd);

        var addedUserChatDto = _mapper.Map<UserChatDto>(addedUserChatEntity);
        return addedUserChatDto;
    }

    public async Task DeleteUserChatAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var userChatEntity = await _repository.GetById(id);

        if (userChatEntity == null)
        {
            throw new NotFound404Exception("UserChat", id.ToString());
        }
        #endregion

        await _repository.Delete(userChatEntity);
    }

    public async Task<UserChatDto> UpdateUserChatAsync(UserChatDto userChatDto)
    {
        #region Check and reject if doesn't exist
        var userChatEntity = await _repository.GetById(userChatDto.ChatId);

        if (userChatEntity == null)
        {
            throw new NotFound404Exception("UserChat", userChatDto.ChatId.ToString());
        }
        #endregion

        var userChatEntityToUpdate = _mapper.Map<UserChatEntity>(userChatDto);

        var updatedUserChatEntity = await _repository.Update(userChatEntityToUpdate);

        var updatedUserChatDto = _mapper.Map<UserChatDto>(updatedUserChatEntity);
        return updatedUserChatDto;
    }
}