using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services.Interfaces;

public interface IAttachmentService
{
    Task<AttachmentDto> GetAttachmentAsync(Guid id);
    Task<IEnumerable<AttachmentDto>> GetAllAttachmentsAsync();
    Task<AttachmentDto> UpdateAttachmentAsync(AttachmentDto attachmentDto);
    Task<AttachmentDto> AddAttachmentAsync(AttachmentDto attachmentDto);
    Task DeleteAttachmentAsync(Guid id);
}