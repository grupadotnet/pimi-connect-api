﻿using Microsoft.AspNetCore.Http;
using pimi_connect_api.Controllers;
using pimi_connect_api.Exceptions;
using pimi_connect_api.UnitTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pimi_connect_api.UnitTests.ControllersTests
{
    public class ChatControllerTest : ControllerUnitTestsBase<ChatDto>
    {
        private ChatController _chatController;

        private new async Task Arrange(bool fillDb = true)
        {
            await base.Arrange();

            if (fillDb)
            {
                await Helper.FillChatsTable(ExistingIds);
            }

            // initialize Controller
            var chatService = new ChatService(TestDbContext, Mapper);
            _chatController = new ChatController(chatService);
        }

        [Theory]
        [InlineData(true)] // should successfully return chat with the given id
        [InlineData(false)] // should return status 404
        public async void GetAsyncTest(bool idExists)
        {
            // Arrange
            await Arrange();

            // Act && Assert
            if (idExists)
            {
                var result = await _chatController.GetAsync(ExistingId);
                var resultTuple = GetStatusAndContentFromResult(result);

                Assert.NotNull(resultTuple.Content);
                Assert.Equal(ExistingId, resultTuple.Content.Id);
                Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
            }
            else
            {
                await Assert.ThrowsAsync<NotFound404Exception>(() =>
                                   _chatController.GetAsync(NotExistingId));
            }
        }

        [Theory]
        [InlineData(true)] // should return all dtos from db
        [InlineData(false)]
        public async void GetAllAsyncTest(bool hasUserInDb)
        {
            // Arrange
            await Arrange(hasUserInDb);

            // Act
            var result = await _chatController.GetAllAsync();
            var resultTuple = GetStatusAndListFromResult(result);

            // Assert
            Assert.NotNull(resultTuple.List);

            if (hasUserInDb)
            {
                Assert.Equal(ExistingIds.Count, resultTuple.List.Count);
            }
            else
            {
                Assert.Equal(new List<ChatDto>(), resultTuple.List);
            }
        }

        [Theory]
        [InlineData(true)] // should successfully return the chat with given name
        [InlineData(false)] // should return status 404
        public async void UpdateASyncTest(bool chatExists)
        {
            await Arrange();

            var newChatName = "updated";

            var chatToUpdate = GenerateChatDto(Mapper, chatExists ? ExistingId : NotExistingId);
            chatToUpdate.Name = newChatName;

            if (chatExists)
            {
                var result = await _chatController.UpdateAsync(chatToUpdate);
                var resultTuple = GetStatusAndContentFromResult(result);

                var chatInDb = await TestDbContext
                    .Chats
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == chatToUpdate.Id);

                Assert.NotNull(resultTuple.Content);
                Assert.NotNull(chatInDb);

                Assert.Equal(chatToUpdate.Name, resultTuple.Content.Name);
                Assert.Equal(chatToUpdate.Name, chatInDb.Name);

                Assert.Equal(chatToUpdate.Id, resultTuple.Content.Id);
                Assert.Equal(chatToUpdate.Id, chatInDb.Id);

                /*Assert.Equal(chatToUpdate.Thumbnail?.Id, resultTuple.Content.Thumbnail?.Id);
                Assert.Equal(chatToUpdate.Thumbnail?.Id, chatInDb.Thumbnail?.Id);

                Assert.Equal(chatToUpdate.ThumbnailKey, resultTuple.Content.ThumbnailKey);
                Assert.Equal(chatToUpdate.ThumbnailKey, chatInDb.ThumbnailKey);
*/
                Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
            }
            else
            {
                await Assert.ThrowsAsync<NotFound404Exception>(() =>
                                                  _chatController.UpdateAsync(chatToUpdate));
            }
        }

        [Theory]
        [InlineData(true)] // should successfully add the chat
        [InlineData(false)] // should return status 400
        public async void AddAsyncTest(bool idExists)
        {
            // Arrange
            await Arrange();

            var chatToAdd = new ChatDto
            {
                Id = idExists ? ExistingId : NotExistingId,
                Name = GenerateUserName(),
                Thumbnail = new AttachmentDto
                {
                    Id = Guid.NewGuid(),
                    ObjectId = Guid.NewGuid(),
                    Type = "image/png",
                    Path = "path/to/file",
                    TableName = "Chats",
                },
                ThumbnailKey = Guid.NewGuid(),
            };

            // Act && Assert
            if (!idExists)
            {
                var result = await _chatController.AddAsync(chatToAdd);
                var resultTuple = GetStatusAndContentFromResult(result);

                var chatInDb = await TestDbContext
                    .Chats
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == chatToAdd.Id);

                Assert.NotNull(resultTuple.Content);
                Assert.NotNull(chatInDb);

                Assert.Equal(chatToAdd.Name, resultTuple.Content.Name);
                Assert.Equal(chatToAdd.Name, chatInDb.Name);

                Assert.Equal(chatToAdd.Id, resultTuple.Content.Id);
                Assert.Equal(chatToAdd.Id, chatInDb.Id);

                /*Assert.Equal(chatToAdd.Thumbnail.Id, resultTuple.Content.Thumbnail?.Id);
                Assert.Equal(chatToAdd.Thumbnail.Id, chatInDb.Thumbnail?.Id);

                Assert.Equal(chatToAdd.ThumbnailKey, resultTuple.Content.ThumbnailKey);
                Assert.Equal(chatToAdd.ThumbnailKey, chatInDb.ThumbnailKey);*/

                Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
            }
            else
            {
                await Assert.ThrowsAsync<BadRequest400Exception>(() =>
                _chatController.AddAsync(chatToAdd));
            }
        }

        [Theory]
        [InlineData(true)] // should successfully delete the chat
        [InlineData(false)] // should return status 404
        public async void DeleteAsyncTest(bool chatExists)
        {
            await Arrange();

            if (chatExists)
            {
                var result = await _chatController.DeleteAsync(ExistingId);
                var resultTuple = GetStatusAndContentFromResult(result);

                var chatInDb = await TestDbContext
                    .Chats
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == ExistingId);

                Assert.Null(resultTuple.Content);
                Assert.Null(chatInDb);

                Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
            }
            else
            {
                await Assert.ThrowsAsync<NotFound404Exception>(() =>
                                                  _chatController.DeleteAsync(NotExistingId));
            }
        }
        
    }
}