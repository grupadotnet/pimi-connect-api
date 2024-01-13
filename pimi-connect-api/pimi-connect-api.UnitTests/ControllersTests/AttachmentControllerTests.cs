using Microsoft.AspNetCore.Http;
using pimi_connect_api.Exceptions;
using pimi_connect_api.UnitTests.Base;

namespace pimi_connect_api.UnitTests.ControllersTests;

[Collection("TestsWhichUseDatabase_pimi-connect-test")]
public class AttachmentControllerTests : ControllerUnitTestsBase<AttachmentDto>
{
    private AttachmentController _attachmentController;
    
    private new async Task Arrange(bool fillDb = true)
    {
        await base.Arrange();

        if (fillDb)
        {
            await Helper.FillAttachmentsTable(ExistingIds);
        }
        
        // initialize Controller
        var attachmentService = new AttachmentService(TestDbContext, Mapper);
        _attachmentController = new AttachmentController(attachmentService);
    }
    
    [Theory]
    [InlineData(true)] // should successfully return attachment with the given id
    [InlineData(false)] // should return status 404
    public async void GetAsyncTest(bool idExists)
    {
        // Arrange
        await Arrange();
    
        // Act && Assert
        if (idExists)
        {
            var result = await _attachmentController.GetAsync(ExistingId);
            var resultTuple = GetStatusAndContentFromResult(result);
            
            Assert.NotNull(resultTuple.Content);
            Assert.Equal(ExistingId, resultTuple.Content.Id);
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => 
                _attachmentController.GetAsync(NotExistingId));
        }
    }
    
    [Theory]
    [InlineData(true)] // should return all dtos from db
    [InlineData(false)] // should return an empty list
    public async void GetAllAsyncTest(bool hasAttachmentsInDb)
    {
        // Arrange
        await Arrange(hasAttachmentsInDb);

        // Act && Assert
        var result = await _attachmentController.GetAllAsync();
        var resultTuple = GetStatusAndListFromResult(result);
        
        Assert.NotNull(resultTuple.List);
        
        if (hasAttachmentsInDb)
        {
            Assert.Equal(EntitiesCount, resultTuple.List.Count);
        }
        else
        {
            Assert.Equal(new List<AttachmentDto>(), resultTuple.List);
        }
    }
    
    [Theory]
    [InlineData(true)] // should return successfully updated AttachmentDto
    [InlineData(false)] // should return status 404
    public async void UpdateAsyncTest(bool attachmentExists)
    {
        // Arrange
        await Arrange();

        var newPath = "/updated/path";

        var attachmentToUpdate = GenerateAttachmentDto(Mapper, 
            attachmentExists ? ExistingId : NotExistingId);
        attachmentToUpdate.Path = newPath;

        // Act && Assert
        if (attachmentExists)
        {
            var result = await _attachmentController.UpdateAsync(attachmentToUpdate);
            var resultTuple = GetStatusAndContentFromResult(result);
            
            var attachmentInDb = await TestDbContext
                .Attachments
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == attachmentToUpdate.Id);
            
            Assert.NotNull(resultTuple.Content);
            Assert.NotNull(attachmentInDb);
            
            Assert.Equal(attachmentToUpdate.Path, resultTuple.Content.Path);
            Assert.Equal(attachmentToUpdate.Path, attachmentInDb.Path);
            
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => 
                _attachmentController.UpdateAsync(attachmentToUpdate));
        }
        
    }
    
    [Theory]
    [InlineData(false)] // should successfully add attachment into the db
    [InlineData(true)] // should return status 400
    public async void AddAsyncTest(bool idAlreadyExists)
    {
        // Arrange
        await Arrange();

        var attachmentToAdd = GenerateAttachmentDto(Mapper, 
            idAlreadyExists ? ExistingId : NotExistingId);
        
        // Act && Assert
        if (!idAlreadyExists)
        {
            var result = await _attachmentController.AddAsync(attachmentToAdd);
            var resultTuple = GetStatusAndContentFromResult(result);

            var attachmentInDb = await TestDbContext
                .Attachments
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == attachmentToAdd.Id);
            
            Assert.NotNull(resultTuple.Content);
            Assert.NotNull(attachmentInDb);
            
            Assert.Equal(attachmentToAdd.Extension, resultTuple.Content.Extension);
            Assert.Equal(attachmentToAdd.Path, attachmentInDb.Path);
            
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<BadRequest400Exception>(() => 
                _attachmentController.AddAsync(attachmentToAdd));
        }
    }
    
    [Theory]
    [InlineData(true)] // should successfully delete attachment from the db and return status 204
    [InlineData(false)] // should return status 404
    public async void DeleteAsyncTest(bool attachmentExists)
    {
        // Arrange
        await Arrange();

        // Act && Assert
        if (attachmentExists)
        {
            var result = await _attachmentController.DeleteAsync(ExistingId);
            var resultTuple = GetStatusAndContentFromResult(result);

            var attachmentInDb = await TestDbContext
                .Attachments
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == ExistingId);
            
            Assert.Equal(null, attachmentInDb);
            Assert.Equal(StatusCodes.Status204NoContent, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => 
                _attachmentController.DeleteAsync(NotExistingId));
        }
    }
}