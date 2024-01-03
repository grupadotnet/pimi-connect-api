using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interface;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<UserDto> GetUserAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(id);

        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("User", id.ToString());
        }
        #endregion

        var userDto = _mapper.Map<UserDto>(checkResult.Entity);
        return userDto;
    }
    
    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var userEntity = await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => 
                u.Email.ToLower().Trim() == email.ToLower().Trim());

        if (userEntity == null)
        {
            throw new NotFound404Exception(
                $"User with email {email} not found!");
        }

        var userDto = _mapper.Map<UserDto>(userEntity);
        
        return userDto;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var userEntities = await _dbContext
            .Users
            .AsNoTracking()
            .ToListAsync();
        
        var userDtoList = _mapper.Map<List<UserDto>>(userEntities);
        return userDtoList;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(userDto.Id);
        
        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("User", userDto.Id.ToString());
        }
        #endregion

        var userEntityToUpdate = _mapper.Map<UserEntity>(userDto);
        
        var updatedUserEntity = _dbContext.Update(userEntityToUpdate);
        await _dbContext.SaveChangesAsync();

        var updatedUserDto = _mapper.Map<UserDto>(updatedUserEntity.Entity);
        return updatedUserDto;
    }

    public async Task<UserDto> AddUserAsync(UserDto userDto)
    {
        #region Check and reject if already exists
        var checkResultById = await CheckIfExistsAndReturn(userDto.Id);
        
        if (checkResultById.Exists)
        {
            throw new BadRequest400Exception(
                $"User with id {userDto.Id} already exists.");
        }
        
        var checkResultByEmail = await CheckIfExistsAndReturn(userDto.Email);
        
        if (checkResultByEmail.Exists)
        {
            throw new BadRequest400Exception(
                $"User with email {userDto.Email} already exists.");
        }
        #endregion
        
        var userEntityToAdd = _mapper.Map<UserEntity>(userDto);
        
        var addedUserEntity = await _dbContext.AddAsync(userEntityToAdd);
        await _dbContext.SaveChangesAsync();

        var addedUserDto = _mapper.Map<UserDto>(addedUserEntity.Entity);
        return addedUserDto;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(id);
        
        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("User", id.ToString());
        }
        #endregion

        _dbContext.Users.Remove(checkResult.Entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<(bool Exists, UserEntity? Entity)> CheckIfExistsAndReturn(Guid id)
    {
        var userEntity = await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return (userEntity != null, userEntity);
    }

    private async Task<(bool Exists, UserEntity? Entity)> CheckIfExistsAndReturn(string email)
    {
        var userEntity = await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        return (userEntity != null, userEntity);
    }
}