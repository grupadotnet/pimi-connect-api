using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        #region Public Methods
        public ChatService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ChatDto> GetChatAsync(Guid id)
        {
            var checkResult = await CheckIfExistsAndReturn(id);

            if(!checkResult.Exists)
            {
                throw new NotFound404Exception("Chat", id.ToString());
            }

            ChatDto chatDto = _mapper.Map<ChatDto>(checkResult.Entity);
            return chatDto;
        }

        public async Task<IEnumerable<ChatDto>> GetAllChatsAsync()
        {
            var chatEntities = await _dbContext
                .Chats
                .AsNoTracking()
                .ToListAsync();

            var chatDtoList = _mapper.Map<IEnumerable<ChatDto>>(chatEntities);
            return chatDtoList;
        }

        public async Task<ChatDto> AddChatAsync(ChatDto chatDto)
        {
            var checkResult = await CheckIfExistsAndReturn(chatDto.Id);

            if(checkResult.Exists)
            {
                throw new BadRequest400Exception($"Chat with id {chatDto.Id} already exists.");
            }

            ChatEntity chatEntityToAdd = _mapper.Map<ChatEntity>(chatDto);

            var addedChatEntity = await _dbContext.AddAsync(chatEntityToAdd);
            await _dbContext.SaveChangesAsync();

            var addedChatDto = _mapper.Map<ChatDto>(addedChatEntity.Entity);
            return addedChatDto;
        }

        public async Task DeleteChatAsync(Guid id)
        {
            var checkResult = await CheckIfExistsAndReturn(id);

            if(!checkResult.Exists)
            {
                throw new NotFound404Exception("Chat", id.ToString());
            }

            _dbContext.Chats.Remove(checkResult.Entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ChatDto> UpdateChatAsync(ChatDto chatDto)
        {
            var checkResult = await CheckIfExistsAndReturn(chatDto.Id);

            if(!checkResult.Exists)
            {
                throw new NotFound404Exception("Chat", chatDto.Id.ToString());
            }

            ChatEntity chatEntityToUpdate = _mapper.Map<ChatEntity>(chatDto);

            var updatedChatEntity = _dbContext.Update(chatEntityToUpdate);
            await _dbContext.SaveChangesAsync();

            var updatedChatDto = _mapper.Map<ChatDto>(updatedChatEntity.Entity);
            return updatedChatDto;
        }
        #endregion

        #region Private Methods

        private async Task<(bool Exists, ChatEntity? Entity)> CheckIfExistsAndReturn(Guid id)
        {
            var chatEntity = await _dbContext
                .Chats
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return (chatEntity != null, chatEntity);
        }

        #endregion
    }
}
