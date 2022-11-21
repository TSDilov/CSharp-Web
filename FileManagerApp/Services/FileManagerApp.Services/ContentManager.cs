namespace FileManagerApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using FileManagerApp.Data.Common.Repositories;
    using FileManagerApp.Data.Models;
    using FileManagerApp.Services.Mapping;
    using FileManagerApp.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using OneBitSoftware.Utilities;
    using OneBitSoftware.Utilities.Errors;

    public class ContentManager : IContentManager
    {
        private readonly IRepository<StreamInfoBaseModel> fileRepository;

        public ContentManager(IRepository<StreamInfoBaseModel> fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<OperationResult> DeleteAsync(string id)
        {
            var operationResult = new OperationResult();
            try
            {
                var file = await this.fileRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);
                this.fileRepository.Delete(file);
                await this.fileRepository.SaveChangesAsync();
                operationResult.AddSuccessMessage("File delete!");
            }
            catch (Exception ex)
            {
                IOperationError error = new OperationError();
                error.Message = ex.Message;
                operationResult.Errors.Add(error);
            }

            return operationResult;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await this.fileRepository.All()
                .AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await this.fileRepository.All()
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetAsync<T>(string id)
        {
            return await this.fileRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<byte[]> GetBytesAsync(string id)
        {
            var file = await this.fileRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return file.Content;
        }

        public async Task<int> GetHashAsync(string id)
        {
            var file = await this.fileRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return file.GetHashCode();
        }

        public async Task<OperationResult> StoreAsync(StreamInfoViewModel fileContent)
        {
            var operationResult = new OperationResult();
            try
            {
                var newFile = new StreamInfoBaseModel
                {
                    FileName = fileContent.FileName,
                    Description = fileContent.Description,
                    Length = fileContent.Length,
                    Content = fileContent.Content,
                };

                await this.fileRepository.AddAsync(newFile);
                await this.fileRepository.SaveChangesAsync();
                operationResult.AddSuccessMessage("File added!");
            }
            catch (Exception ex)
            {
                IOperationError error = new OperationError();
                error.Message = ex.Message;
                operationResult.Errors.Add(error);
            }

            return operationResult;
        }

        public async Task<OperationResult> UpdateAsync(string id, StreamInfoViewModel fileContent)
        {
            var operationResult = new OperationResult();

            try
            {
                var file = await this.fileRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

                file.FileName = fileContent.FileName;
                file.Description = fileContent?.Description;

                await this.fileRepository.SaveChangesAsync();
                operationResult.AddSuccessMessage("File updated!");
            }
            catch (Exception ex)
            {
                IOperationError error = new OperationError();
                error.Message = ex.Message;
                operationResult.Errors.Add(error);
            }

            return operationResult;
        }
    }
}
