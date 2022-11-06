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

        public TrainerService(IDeletableEntityRepository<Trainer> trainerRepository)
        {
            this.trainerRepository = trainerRepository;
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

            var allowedExtensions = new[] { "jpg", "png", "gif" };
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

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await this.trainerRepository
                .AllAsNoTracking()
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
    }
}
