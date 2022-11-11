namespace SportApp.Services.Data
{
    using SportApp.Data.Common.Repositories;
    using SportApp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly IDeletableEntityRepository<Message> messageRepository;

        public MessageService(IDeletableEntityRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task CreateAsync(Message input)
        {
            await this.messageRepository.AddAsync(input);
            await this.messageRepository.SaveChangesAsync();
        }

        public IEnumerable<Message> GetAll()
        {
            return this.messageRepository.All().ToList();
        }
    }
}
