using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
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

        public Task<MessageDto> GetMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
        {
            var messageEntities = await _dbContext
               .Messages
               .AsNoTracking()
               .ToListAsync();

            var messageDtoList = _mapper.Map<List<MessageDto>>(messageEntities);
            return messageDtoList;
        }

        public Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public bool MessageExistsAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
