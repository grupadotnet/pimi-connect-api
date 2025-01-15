using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Exceptions;
using pimi_connect_api.Message;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;
using pimi_connect_app.Data.Repository;

namespace pimi_connect_api.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly MessageRepository _repository;

        public MessageService(AppDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _repository = new MessageRepository(dbContext);
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
            var messageEntities = await _repository.GetAll();

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
        private async Task<MessageEntity?> CheckIfExistsAndReturn(Guid id)
        {
            if (!await _repository.MessageExists(id))
            {
                throw new NotFound404Exception("Not found message by given Id: ", id.ToString());
            }
            return await _repository.GetById(id);     
        }
    }
}
