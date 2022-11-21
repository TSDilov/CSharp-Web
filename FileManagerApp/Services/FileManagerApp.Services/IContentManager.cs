namespace FileManagerApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using FileManagerApp.Data.Models;
    using FileManagerApp.Web.ViewModels;
    using OneBitSoftware.Utilities;

    public interface IContentManager
    {
        Task<OperationResult> StoreAsync(StreamInfoViewModel fileContent);

        Task<IEnumerable<T>> GetAll<T>();

        Task<bool> ExistsAsync(string id);

        Task<T> GetAsync<T>(string id);

        Task<OperationResult<byte[]>> GetBytesAsync(string id);

        Task<OperationResult> UpdateAsync(string id, StreamInfoViewModel fileContent);

        Task<OperationResult> DeleteAsync(string id);

        Task<OperationResult<string>> GetHashAsync(string id);
    }
}
