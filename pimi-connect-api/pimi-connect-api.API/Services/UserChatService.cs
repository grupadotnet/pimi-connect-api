using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services;

public class UserChatService : IUserChatService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserChatService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserChatDto> GetUserChatAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(id);

        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("UserChat", id.ToString());
        }
        #endregion

        var userChaDto = _mapper.Map<UserChatDto>(checkResult.Entity);
        return userChaDto;
    }

    public async Task<IEnumerable<UserChatDto>> GetAllUserChatsAsync()
    {
        var userChatEntities = await _dbContext
            .UserChats
            .AsNoTracking()
            .ToListAsync();
        
        var userChatDtoList = _mapper.Map<List<UserChatDto>>(userChatEntities);
        return userChatDtoList;
    }

    public async Task<UserChatDto> AddUserChatAsync(UserChatDto userChatDto)
    {
        #region Check and reject if already exists
        
        var checkResultById = await CheckIfExistsAndReturn(userChatDto.ChatId); //TODO: userChatDto should have changed ID
        
        if (checkResultById.Exists)
        {
            throw new BadRequest400Exception(
                $"UserChat with id {userChatDto.ChatId} already exists.");
        }
        #endregion
        
        var userChatEntityToAdd = _mapper.Map<UserChatEntity>(userChatDto);
        
        var addedUserChatEntity = await _dbContext.AddAsync(userChatEntityToAdd);
        await _dbContext.SaveChangesAsync();

        var addedUserChatDto = _mapper.Map<UserChatDto>(addedUserChatEntity.Entity);
        return addedUserChatDto;
    }

    public async Task DeleteUserChatAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(id);
        
        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("UserChat", id.ToString());
        }
        #endregion

        _dbContext.UserChats.Remove(checkResult.Entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserChatDto> UpdateUserChatAsync(UserChatDto userChatDto)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(userChatDto.ChatId);
        
        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("UserChat", userChatDto.ChatId.ToString());
        }
        #endregion

        var userChatEntityToUpdate = _mapper.Map<UserChatEntity>(userChatDto);
        
        var updatedUserChatEntity = _dbContext.Update(userChatEntityToUpdate);
        await _dbContext.SaveChangesAsync();

        var updatedUserChatDto = _mapper.Map<UserChatDto>(updatedUserChatEntity.Entity);
        return updatedUserChatDto;
    }

    private async Task<(bool Exists, UserChatEntity? Entity)> CheckIfExistsAndReturn(Guid id)
    {
        var userChatEntity = await _dbContext
            .UserChats
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return (userChatEntity != null, userChatEntity);
    }
}