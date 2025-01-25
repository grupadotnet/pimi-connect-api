using pimi_connect_api.Exceptions;
using pimi_connect_api.Message;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_api.Tests.UnitTests.Base;
using Xunit.Abstractions;

namespace pimi_connect_api.Tests.UnitTests;

public class MessageServiceTests : UnitTestsBase
{
    private readonly Mock<IMessageRepository> _mockRepository;
    private readonly IMessageService _messageService;
    
    public MessageServiceTests(ITestOutputHelper output) : base(output)
    {
        _mockRepository = new Mock<IMessageRepository>();
        _messageService = new MessageService(Mapper, _mockRepository.Object);
    }
    
    [Fact]
    public async Task GetMessageAsync_GetsExistingData()
    {
        // Arrange
        _mockRepository
            .Setup(x => x.MessageExists(DummyId))
            .ReturnsAsync(true);
        
        _mockRepository
            .Setup(x => x.GetById(DummyId))
            .ReturnsAsync(new MessageEntity()
            {
                Id = DummyId,
                Content = DummyString
            });
        
        // Act
        var result = await _messageService.GetMessageAsync(DummyId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(DummyId, result.Id);
        Assert.Equal(DummyString, result.Content);
    }
    
    [Fact]
    public async Task GetMessageAsync_ThrowsNotFoundForNonExistingData()
    {
        // Arrange
        _mockRepository
            .Setup(x => x.MessageExists(DummyId))
            .ReturnsAsync(false);
        
        // Act & Assert
        await Assert.ThrowsAsync<NotFound404Exception>(() => _messageService.GetMessageAsync(DummyId));
    }
    
    [Fact]
    public async Task GetAllMessagesAsync_GetsAllData()
    {
        // Arrange
        var data = new List<MessageEntity>
        {
            new()
            {
                Id = DummyIds[0],
                Content = DummyStrings[0]
            },
            new()
            {
                Id = DummyIds[1],
                Content = DummyStrings[1]
            },
            new()
            {
                Id = DummyIds[2],
                Content = DummyStrings[2]
            }
        };
        
        _mockRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync(data);
        
        // Act 
        var result = (await _messageService.GetAllMessagesAsync()).ToList();
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(data.Count, result.Count);
        Assert.Equal(data[0].Id, result[0].Id);
    }
}