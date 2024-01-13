using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services;

public class AttachmentService : IAttachmentService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public AttachmentService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<AttachmentDto> GetAttachmentAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(id);

        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("Attachment", id.ToString());
        }
        #endregion

        var attachmentDto = _mapper.Map<AttachmentDto>(checkResult.Entity);
        return attachmentDto;
    }
    
    public async Task<IEnumerable<AttachmentDto>> GetAllAttachmentsAsync()
    {
        var attachmentEntities = await _dbContext
            .Attachments
            .AsNoTracking()
            .ToListAsync();
        
        var attachmentDtoList = _mapper.Map<List<AttachmentDto>>(attachmentEntities);
        return attachmentDtoList;
    }

    public async Task<AttachmentDto> UpdateAttachmentAsync(AttachmentDto attachmentDto)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(attachmentDto.Id);
        
        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("Attachment", attachmentDto.Id.ToString());
        }
        #endregion

        var attachmentEntityToUpdate = _mapper.Map<AttachmentEntity>(attachmentDto);
        
        var updatedAttachmentEntity = _dbContext.Update(attachmentEntityToUpdate);
        await _dbContext.SaveChangesAsync();

        var updatedAttachmentDto = _mapper.Map<AttachmentDto>(updatedAttachmentEntity.Entity);
        return updatedAttachmentDto;
    }

    public async Task<AttachmentDto> AddAttachmentAsync(AttachmentDto attachmentDto)
    {
        var checkResultById = await CheckIfExistsAndReturn(attachmentDto.Id);
        
        if (checkResultById.Exists)
        {
            throw new BadRequest400Exception(
                $"Attachment with id {attachmentDto.Id} already exists.");
        }
        
        var attachmentEntityToAdd = _mapper.Map<AttachmentEntity>(attachmentDto);
        
        var addedAttachmentEntity = await _dbContext.AddAsync(attachmentEntityToAdd);
        await _dbContext.SaveChangesAsync();

        var addedAttachmentDto = _mapper.Map<AttachmentDto>(addedAttachmentEntity.Entity);
        return addedAttachmentDto;
    }

    public async Task DeleteAttachmentAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var checkResult = await CheckIfExistsAndReturn(id);
        
        if (!checkResult.Exists)
        {
            throw new NotFound404Exception("Attachment", id.ToString());
        }
        #endregion

        _dbContext.Attachments.Remove(checkResult.Entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<(bool Exists, AttachmentEntity? Entity)> CheckIfExistsAndReturn(Guid id)
    {
        var attachmentEntity = await _dbContext
            .Attachments
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return (attachmentEntity != null, attachmentEntity);
    }
}