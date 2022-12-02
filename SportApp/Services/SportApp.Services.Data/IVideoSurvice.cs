namespace SportApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SportApp.Web.ViewModels;
    using SportApp.Web.ViewModels.Trainers;

    public interface IVideoSurvice
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(VideoModel model, string imagePath);

        Task DeleteAsync(string id);
    }
}
