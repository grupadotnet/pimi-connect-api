using pimi_connect_api.Attachment;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Attachment;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_api.Tests.UnitTests.Base;
using pimi_connect_api.UnitTests.Shared;
using Xunit.Abstractions;

namespace pimi_connect_api.Tests.UnitTests;

public class AttachmentServiceTests : UnitTestsBase
{
    private readonly Mock<IAttachmentRepository> _mockRepository;
    private readonly IAttachmentService _attachmentService;

    private readonly AttachmentEntity _attachmentEntity;
    private readonly AttachmentDto _attachmentDto;
    
    public AttachmentServiceTests(ITestOutputHelper output) : base(output)
    {
        _mockRepository = new Mock<IAttachmentRepository>();
        _attachmentService = new AttachmentService(Mapper, _mockRepository.Object);

        _attachmentEntity = GenerateAttachmentEntity(DummyId);
        _attachmentDto = GenerateAttachmentDto(Mapper);
    }
    
    [Fact]
    public async Task GetAttachmentAsync_GetsExistingData()
    {
        // Arrange
        _mockRepository
            .Setup(x => x.GetById(DummyId))
            .ReturnsAsync(new AttachmentEntity()
            {
                Id = DummyId,
                PublicName = DummyString
            });
        
        // Act
        var result = await _attachmentService.GetAttachmentAsync(DummyId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(DummyId, result.Id);
        Assert.Equal(DummyString, result.PublicName);
    }
    
    [Fact]
    public async Task GetAttachmentAsync_ThrowsNotFoundForNonExistingData()
    {
        // Arrange
        _mockRepository
            .Setup(x => x.GetById(DummyId))
            .ReturnsAsync((AttachmentEntity?)null);
        
        // Act & Assert
        await Assert.ThrowsAsync<NotFound404Exception>(() => _attachmentService.GetAttachmentAsync(DummyId));
    }
    
    [Fact]
    public async Task GetAllAttachmentsAsync_GetsAllData()
    {
        // Arrange
        var data = new List<AttachmentEntity>
        {
            _attachmentEntity,
            new()
            {
                Id = DummyIds[0],
                PublicName = DummyStrings[0]
            },
            new()
            {
                Id = DummyIds[1],
                PublicName = DummyStrings[1]
            },
            new()
            {
                Id = DummyIds[2],
                PublicName = DummyStrings[2]
            },
        };
        
        _mockRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync(data);
        
        // Act 
        var result = (await _attachmentService.GetAllAttachmentsAsync()).ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(data.Count, result.Count);
        Assert.Equal(data[0].Id, result[0].Id);
    }

    [Fact]
    public async Task UpdateAttachmentAsync_InvokesUpdateAndReturnsUpdatedResult()
    {
        // Arrange
        _mockRepository
            .Setup(x => x.GetById(DummyId))
            .ReturnsAsync(_attachmentEntity);

        var updatedAttachmentEntity = new AttachmentEntity()
        {
            Id = DummyId,
            PublicName = DummyString
        };

        var updateAttachmentDto = Mapper.Map<AttachmentDto>(updatedAttachmentEntity);

        var updateInvoked = false;
        _mockRepository
            .Setup(x => x.Update(It.IsAny<AttachmentEntity>()))
            .ReturnsAsync(updatedAttachmentEntity)
            .Callback(() => updateInvoked = true);

        // Act
        var result = await _attachmentService.UpdateAttachmentAsync(updateAttachmentDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(updateInvoked);
        Assert.Equal(updatedAttachmentEntity.Id, result.Id);
        Assert.Equal(updatedAttachmentEntity.PublicName, result.PublicName);
    }

    [Fact]
    public async Task AddAttachmentAsync_CallsRepositoryAndReturnsDto()
    {
        // Arrange
        var attachmentDto = new AttachmentDto()
        {
            PublicName = DummyString
        };

        var addInvoked = false;
        _mockRepository
            .Setup(x => x.Add(It.IsAny<AttachmentEntity>()))
            .ReturnsAsync(_attachmentEntity)
            .Callback(() => addInvoked = true);

        // Act
        var result = await _attachmentService.AddAttachmentAsync(attachmentDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(addInvoked);
        Assert.Equal(_attachmentEntity.Id, result.Id);
        Assert.Equal(_attachmentEntity.PublicName, result.PublicName);
    }

    [Fact]
    public async Task DeleteAttachmentAsync_CallsRepository()
    {
        // Arrange
        _mockRepository
            .Setup(x => x.GetById(DummyId))
            .ReturnsAsync(_attachmentEntity);
        
        var deleteInvoked = false;
        _mockRepository
            .Setup(x => x.Delete(_attachmentEntity))
            .Callback(() => deleteInvoked = true);
        
        // Act
        await _attachmentService.DeleteAttachmentAsync(DummyId);

        // Assert
        Assert.True(deleteInvoked);
    }
}