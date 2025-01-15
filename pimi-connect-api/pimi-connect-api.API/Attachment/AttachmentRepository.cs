using Microsoft.EntityFrameworkCore;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Repository;

namespace pimi_connect_api.Attachment;

public class AttachmentRepository : IRepository<AttachmentEntity>
{
    private readonly AppDbContext _dbContext;
    
    public AttachmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<AttachmentEntity>> GetAll()
    {
        return await _dbContext
            .Attachments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AttachmentEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Attachments
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<AttachmentEntity> Add(AttachmentEntity entity)
    {
        var addedAttachment = await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return addedAttachment.Entity;
    }

    public async Task<AttachmentEntity> Update(AttachmentEntity entity)
    {
        var updatedAttachment = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return updatedAttachment.Entity;
    }

    public async Task Delete(AttachmentEntity entity)
    {
        _dbContext.Attachments.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}