namespace SportApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SportApp.Data.Common.Repositories;
    using SportApp.Data.Models;
    using SportApp.Services.Mapping;
    using SportApp.Web.ViewModels.GroupTrainings;

    public class GroupTrainingsService : IGroupTrainingsService
    {
        private readonly IDeletableEntityRepository<GroupTraining> groupTrainingRepository;
        private readonly IRepository<Trainer> trainerRepository;

        public GroupTrainingsService(
            IDeletableEntityRepository<GroupTraining> groupTrainingRepository,
            IRepository<Trainer> trainerRepository)
        {
            this.groupTrainingRepository = groupTrainingRepository;
            this.trainerRepository = trainerRepository;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await this.groupTrainingRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();
        }

        public async Task CreateAsync(CreateGroupTrainingInputModel input)
        {
            var trainer = await this.trainerRepository.All()
                .FirstOrDefaultAsync(x => x.Name == input.TrainerName);

            if (trainer == null)
            {
                throw new InvalidOperationException("This trainer don't exist");
            }

            var groupTraining = new GroupTraining
            {
                Name = input.Name,
                Description = input.Description,
                Place = input.Place,
                DaysOfWeek = input.DaysOfWeek,
                StartHour = input.StartHour,
                Trainer = trainer,
            };

            await this.groupTrainingRepository.AddAsync(groupTraining);
            await this.trainerRepository.SaveChangesAsync();
        }
    }
}
