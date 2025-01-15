using Microsoft.AspNetCore.Mvc;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;

    public AttachmentController(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }
    
    [HttpGet]
    [Route("GetAsync")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var attachmentDto = await _attachmentService.GetAttachmentAsync(id);
        
        return StatusCode(StatusCodes.Status200OK, attachmentDto);
    }
    
    [HttpGet]
    [Route("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var attachmentDtoList = await _attachmentService.GetAllAttachmentsAsync();
        
        return StatusCode(StatusCodes.Status200OK, attachmentDtoList);
    }
    
    [HttpPut]
    [Route("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync(AttachmentDto attachmentDto)
    {
        var updatedAttachmentDto = await _attachmentService.UpdateAttachmentAsync(attachmentDto);
        
        return StatusCode(StatusCodes.Status200OK, updatedAttachmentDto);
    }
    
    [HttpPost]
    [Route("AddAsync")]
    public async Task<IActionResult> AddAsync(AttachmentDto attachmentDto)
    {
        var addedAttachmentDto = await _attachmentService.AddAttachmentAsync(attachmentDto);
        
        return StatusCode(StatusCodes.Status200OK, addedAttachmentDto);
    }
    
    [HttpDelete]
    [Route("DeleteAsync")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _attachmentService.DeleteAttachmentAsync(id);
        
        return StatusCode(StatusCodes.Status204NoContent);
    }
}