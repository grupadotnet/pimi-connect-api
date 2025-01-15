using Microsoft.EntityFrameworkCore;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Repository;

namespace pimi_connect_api.Message;

public class MessageRepository : IRepository<MessageEntity>, IMessageRepository
{
    private readonly AppDbContext _dbContext;
    
    public MessageRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<MessageEntity>> GetAll()
    {
        return await _dbContext
            .Messages
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MessageEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Messages
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<MessageEntity> Add(MessageEntity entity)
    {
        var addedMessage = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return addedMessage.Entity;
    }

    public async Task<MessageEntity> Update(MessageEntity entity)
    {
        var updatedMessage = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return updatedMessage.Entity;
    }

    public async Task Delete(MessageEntity entity)
    {
        _dbContext.Messages.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> MessageExists(Guid id)
    {
        return await _dbContext.Messages.AnyAsync(m => m.Id == id);
    }
}