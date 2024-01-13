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
            var chatEntities = _dbContext
                .Chats
                .AsNoTracking()
                .ToListAsync();

            var chatDtoList = _mapper.Map<IEnumerable<ChatDto>>(chatEntities);
            return chatDtoList;
        }

        public Task<ChatDto> AddChatAsync(ChatDto chatDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChatAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ChatDto> UpdateChatAsync(ChatDto chatDto)
        {
            throw new NotImplementedException();
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
