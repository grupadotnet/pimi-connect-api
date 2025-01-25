using AutoMapper;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Attachment;

public class AttachmentService : IAttachmentService
{
    private readonly IMapper _mapper;
    private readonly IAttachmentRepository _repository;

    public AttachmentService(IMapper mapper, IAttachmentRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<AttachmentDto> GetAttachmentAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var attachment = await _repository.GetById(id);

        if (attachment == null)
        {
            throw new NotFound404Exception("Attachment", id.ToString());
        }
        #endregion

        var attachmentDto = _mapper.Map<AttachmentDto>(attachment);
        return attachmentDto;
    }
    
    public async Task<IEnumerable<AttachmentDto>> GetAllAttachmentsAsync()
    {
        var attachmentEntities = await _repository.GetAll();
        
        var attachmentDtoList = _mapper.Map<List<AttachmentDto>>(attachmentEntities);
        return attachmentDtoList;
    }

    public async Task<AttachmentDto> UpdateAttachmentAsync(AttachmentDto attachmentDto)
    {
        #region Check and reject if doesn't exist
        var attachment = await _repository.GetById(attachmentDto.Id);

        if (attachment == null)
        {
            throw new NotFound404Exception("Attachment", attachmentDto.Id.ToString());
        }
        #endregion

        var attachmentEntityToUpdate = _mapper.Map<AttachmentEntity>(attachmentDto);

        var updatedAttachmentEntity = await _repository.Update(attachmentEntityToUpdate);

        var updatedAttachmentDto = _mapper.Map<AttachmentDto>(updatedAttachmentEntity);
        return updatedAttachmentDto;
    }

    public async Task<AttachmentDto> AddAttachmentAsync(AttachmentDto attachmentDto)
    {
        var attachment = await _repository.GetById(attachmentDto.Id);

        if (attachment != null)
        {
            throw new BadRequest400Exception(
                $"Attachment with id {attachmentDto.Id} already exists.");
        }
        
        var attachmentEntityToAdd = _mapper.Map<AttachmentEntity>(attachmentDto);

        var addedAttachmentEntity = await _repository.Add(attachmentEntityToAdd);

        var addedAttachmentDto = _mapper.Map<AttachmentDto>(addedAttachmentEntity);
        return addedAttachmentDto;
    }

    public async Task DeleteAttachmentAsync(Guid id)
    {
        #region Check and reject if doesn't exist
        var attachment = await _repository.GetById(id);

        if (attachment == null)
        {
            throw new NotFound404Exception("Attachment", id.ToString());
        }
        #endregion

        await _repository.Delete(attachment);
    }
}