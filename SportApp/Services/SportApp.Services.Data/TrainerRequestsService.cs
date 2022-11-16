namespace SportApp.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SportApp.Data.Common.Repositories;
    using SportApp.Data.Models;
    using SportApp.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TrainerRequestsService : ITrainerRequestsService
    {
        private readonly IDeletableEntityRepository<RequestTrainer> requestTrainerRepository;

        public TrainerRequestsService(IDeletableEntityRepository<RequestTrainer> requestTrainerRepository)
        {
            this.requestTrainerRepository = requestTrainerRepository;
        }

        public async Task Approved(int id)
        {
            var trainerRequest = await this.requestTrainerRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            trainerRequest.IsApproved = true;
            await this.requestTrainerRepository.SaveChangesAsync();
        }
    }
}
