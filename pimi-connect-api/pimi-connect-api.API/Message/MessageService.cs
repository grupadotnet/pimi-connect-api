using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Task<MessageDto> AddMessageAsync(MessageDto messageDto)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageDto> GetMessageAsync(Guid messageId)
        {
            var messageDto = _mapper.Map<MessageDto>(await CheckIfExistsAndReturn(messageId));
            return messageDto;
        }

        public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
        {
            var messageEntities = await _dbContext
               .Messages
               .ToListAsync();

            var messageDtoList = _mapper.Map<List<MessageDto>>(messageEntities);
            return messageDtoList;
        }

        public Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync()
        {
            throw new NotImplementedException();
        }
        private async Task<MessageEntity?> CheckIfExistsAndReturn(Guid messageId)
        {
            if (!await MessageExists(messageId))
            {
                throw new NotFound404Exception("Not found message by given Id: ", messageId.ToString());
            }
            return await ReturnMessage(messageId);     
        }

        private async Task<bool> MessageExists(Guid messageId)
        {
            return await _dbContext.Messages.AnyAsync(m => m.Id == messageId);
        }

        private async Task<MessageEntity?> ReturnMessage(Guid messageId)
        {
            return await _dbContext.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
        }

        private async Task<bool> Save()
        {
            var saved = await _dbContext.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
