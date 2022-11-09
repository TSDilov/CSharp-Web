namespace SportApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SportApp.Data.Common.Repositories;
    using SportApp.Data.Models;
    using SportApp.Services.Mapping;
    using SportApp.Web.ViewModels.Trainers;

    using static System.Net.Mime.MediaTypeNames;

    public class TrainerService : ITrainerService
    {
        private readonly IDeletableEntityRepository<Trainer> trainerRepository;
        private readonly IRepository<ApplicationUserTrainer> aplicationUserTrainer;

        public TrainerService(
            IDeletableEntityRepository<Trainer> trainerRepository,
            IRepository<ApplicationUserTrainer> aplicationUserTrainer)
        {
            this.trainerRepository = trainerRepository;
            this.aplicationUserTrainer = aplicationUserTrainer;
        }

        public async Task BookTrainerAsync(int id, string userId)
        {
            var trainerUser = await this.aplicationUserTrainer.All()
                .FirstOrDefaultAsync(x => x.TrainerId == id && x.ApplicationUserId == userId);

            if (trainerUser == null)
            {
                trainerUser = new ApplicationUserTrainer
                {
                    TrainerId = id,
                    ApplicationUserId = userId,
                };

                var trainer = await this.trainerRepository.All()
                    .FirstOrDefaultAsync(t => t.Id == id);
                trainer.ApplicationUsersTrainers.Add(trainerUser);
                await this.trainerRepository.SaveChangesAsync();
            }
        }

        public async Task CreateAsync(CreateTrainerInputModel input, string userId, string imagePath)
        {
            var trainer = new Trainer
            {
                Name = input.Name,
                InfoCard = input.InfoCard,
                DateOfBirth = input.DateOfBirth,
                PricePerTraining = input.PricePerTraining,
                CategotyId = input.CategoryId,
            };

            var allowedExtensions = new[] { "jpg", "png", "gif", "jfif" };
            Directory.CreateDirectory($"{imagePath}/recipes/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!allowedExtensions.Contains(extension))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new SportApp.Data.Models.Image
                {
                    Extension = extension,
                };

                trainer.Images.Add(dbImage);
                var physicalPath = $"{imagePath}/trainerss/{dbImage.Id}.{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.trainerRepository.AddAsync(trainer);
            await this.trainerRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await this.trainerRepository.All().FirstOrDefaultAsync(r => r.Id == id);
            this.trainerRepository.Delete(recipe);
            await this.trainerRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage = 12)
        {
            return await this.trainerRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
        }

        public T GetById<T>(int id)
        {
            return this.trainerRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.trainerRepository.All().Count();
        }

        public async Task UpdateAsync(int id, EditTrainerInputModel input)
        {
            var trainer = await this.trainerRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            trainer.Name = input.Name;
            trainer.InfoCard = input.InfoCard;
            trainer.DateOfBirth = input.DateOfBirth;
            trainer.PricePerTraining = input.PricePerTraining;
            trainer.CategotyId = input.CategoryId;

            await this.trainerRepository.SaveChangesAsync();
        }
    }
}
