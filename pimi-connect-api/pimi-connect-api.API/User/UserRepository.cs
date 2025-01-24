using Microsoft.EntityFrameworkCore;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Repository;

namespace pimi_connect_api.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<UserEntity?> GetByEmail(string email)
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
    }

    public async Task<UserEntity> Add(UserEntity entity)
    {
        var addedUser = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return addedUser.Entity;
    }

    public async Task<UserEntity> Update(UserEntity entity)
    {
        var updatedUser = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return updatedUser.Entity;
    }

    public async Task Delete(UserEntity entity)
    {
        _dbContext.Users.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}