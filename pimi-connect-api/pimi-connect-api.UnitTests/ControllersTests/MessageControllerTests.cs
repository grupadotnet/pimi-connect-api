using Microsoft.AspNetCore.Http;
using pimi_connect_api.Exceptions;
using pimi_connect_api.UnitTests.Base;

namespace pimi_connect_api.UnitTests.ControllersTests
{
    public class MessageControllerTests : ControllerUnitTestsBase<MessageDto>
    {
        private MessageController _messageController;

        private new async Task Arrange(bool fillDb = true)
        {
            await base.Arrange();

            if (fillDb)
            {
                await Helper.FillMessagesTable(ExistingIds);
            }

            // initialize Controller
            var messageService = new MessageService(TestDbContext, Mapper);
            _messageController = new MessageController(messageService);
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
                var result = await _messageController.GetAsync(ExistingId);
                var resultTuple = GetStatusAndContentFromResult(result);

                Assert.NotNull(resultTuple.Content);
                Assert.Equal(ExistingId, resultTuple.Content.Id);
                Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
            }
            else
            {
                await Assert.ThrowsAsync<NotFound404Exception>(() =>
                    _messageController.GetAsync(NotExistingId));
            }
        }
        [Theory]
        [InlineData(true)] // should return all dtos from db
        [InlineData(false)] // should return an empty list
        public async void GetAllAsyncTest(bool hasMessagesInDb)
        {
            // Arrange
            await Arrange(hasMessagesInDb);

            // Act && Assert
            var result = await _messageController.GetAllAsync();
            var resultTuple = GetStatusAndListFromResult(result);

            Assert.NotNull(resultTuple.List);

            if (hasMessagesInDb)
            {
                Assert.Equal(EntitiesCount, resultTuple.List.Count);
            }
            else
            {
                Assert.Equal(new List<MessageDto>(), resultTuple.List);
            }
        }

        [Theory]
        [InlineData(false)] // should successfully add message into the db
        [InlineData(true)] // should return status 400
        public async void AddAsyncTest(bool idAlreadyExists)
        {
            // Arrange
            await Arrange();

            var messageToAdd = GenerateMessageDto(Mapper,
                idAlreadyExists ? ExistingId : NotExistingId);

            // Act && Assert
            if (!idAlreadyExists)
            {
                var result = await _messageController.AddAsync(messageToAdd);
                var resultTuple = GetStatusAndContentFromResult(result);

                var messageInDb = await TestDbContext
                    .Messages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == messageToAdd.Id);

                Assert.NotNull(resultTuple.Content);
                Assert.NotNull(messageInDb);

                Assert.Equal(messageToAdd.Content, resultTuple.Content.Content);
                Assert.Equal(messageToAdd.CreatedDate, messageInDb.CreatedDate);

                Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
            }
            else
            {
                await Assert.ThrowsAsync<BadRequest400Exception>(() =>
                    _messageController.AddAsync(messageToAdd));
            }
        }

        [Theory]
        [InlineData(true)] // should successfully delete message from the db and return status 204
        [InlineData(false)] // should return status 404
        public async void DeleteAsyncTest(bool messageExists)
        {
            // Arrange
            await Arrange();

            // Act && Assert
            if (messageExists)
            {
                var result = await _messageController.DeleteAsync(ExistingId);
                var resultTuple = GetStatusAndContentFromResult(result);

                var messageInDb = await TestDbContext
                    .Messages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == ExistingId);

                Assert.Equal(null, messageInDb);
                Assert.Equal(StatusCodes.Status204NoContent, resultTuple.Status);
            }
            else
            {
                await Assert.ThrowsAsync<NotFound404Exception>(() =>
                    _messageController.DeleteAsync(NotExistingId));
            }
        }
    }
}
