using Microsoft.EntityFrameworkCore;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Repository;

namespace pimi_connect_api.UserChat;

public class UserChatRepository : IUserChatRepository
{
    private readonly AppDbContext _dbContext;
    
    public UserChatRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<UserChatEntity>> GetAll()
    {
        return await _dbContext
            .UserChats
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserChatEntity?> GetById(Guid id)
    {
        return await _dbContext
            .UserChats
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserChatEntity> Add(UserChatEntity entity)
    {
        var addedUserChat = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return addedUserChat.Entity;
    }

    public async Task<UserChatEntity> Update(UserChatEntity entity)
    {
        var updatedUserChat = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return updatedUserChat.Entity;
    }

    public async Task Delete(UserChatEntity entity)
    {
        _dbContext.UserChats.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}