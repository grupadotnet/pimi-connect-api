using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.User;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public UserService(IMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<UserDto> GetUserAsync(Guid id)
    {
        #region Check and reject if doesn't exist

        var userEntity = await _repository.GetById(id);

        if (userEntity == null)
        {
            throw new NotFound404Exception("User", id.ToString());
        }
        #endregion

        var userDto = _mapper.Map<UserDto>(userEntity);
        return userDto;
    }
    
    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var userEntity = await _repository.GetByEmail(email);

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
        var userEntities = await _repository.GetAll();
        
        var userDtoList = _mapper.Map<List<UserDto>>(userEntities);
        return userDtoList;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        #region Check and reject if doesn't exist
        var userEntity = await _repository.GetById(userDto.Id);

        if (userEntity == null)
        {
            throw new NotFound404Exception("User", userDto.Id.ToString());
        }
        #endregion

        var userEntityToUpdate = _mapper.Map<UserEntity>(userDto);

        var updatedUserEntity = await _repository.Update(userEntityToUpdate);

        var updatedUserDto = _mapper.Map<UserDto>(updatedUserEntity);
        return updatedUserDto;
    }

    public async Task<UserDto> AddUserAsync(UserDto userDto)
    {
        #region Check and reject if already exists
        var userEntity = await _repository.GetById(userDto.Id);
        if (userEntity != null)
        {
            throw new BadRequest400Exception(
                $"User with id {userDto.Id} already exists.");
        }

        userEntity = await _repository.GetByEmail(userDto.Email);
        if (userEntity != null)
        {
            throw new BadRequest400Exception(
                $"User with email {userDto.Email} already exists.");
        }
        #endregion
        
        var userEntityToAdd = _mapper.Map<UserEntity>(userDto);

        var addedUserEntity = await _repository.Add(userEntityToAdd);

        var addedUserDto = _mapper.Map<UserDto>(addedUserEntity);
        return addedUserDto;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var userEntity = await _repository.GetById(id);

        if (userEntity == null)
        {
            throw new NotFound404Exception("User", id.ToString());
        }
        #endregion

        await _repository.Delete(userEntity);
    }
}