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

        public async Task DeleteAsync(int id)
        {
            var task = this.GetById(id);
            this.dbContext.Tasks.Remove(task);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(CreateTaskViewModel model, int id)
        {
            var taskFromBase = this.GetById(id);
            taskFromBase.Title = model.Title;
            taskFromBase.Description = model.Description;
            taskFromBase.BoardId = model.BoardId;

            await this.dbContext.SaveChangesAsync();
        }

        public TaskA GetById(int id)
        {
            return this.dbContext.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task<TaskDetailsViewModel?> GetTaskByIdAsync(int id)
        {
            return  this.dbContext
                .Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName,
                })
                .FirstOrDefault();
        }
    }
}
