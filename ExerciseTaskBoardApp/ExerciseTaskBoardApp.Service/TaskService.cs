using ExerciseTaskBoardApp.Data;
using ExerciseTaskBoardApp.Data.Models;
using ExerciseTaskBoardApp.ViewModels.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTaskBoardApp.Service
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext dbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateTaskViewModel model, string userId)
        {
            var task = new TaskA()
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                BoardId = model.BoardId,
                OwnerId = userId,
            };

            await this.dbContext.Tasks.AddAsync(task);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
