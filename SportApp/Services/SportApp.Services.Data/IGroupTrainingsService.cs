namespace SportApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SportApp.Web.ViewModels.GroupTrainings;
    using SportApp.Web.ViewModels.Trainers;

    public interface IGroupTrainingsService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task CreateAsync(CreateGroupTrainingInputModel input);

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditGroupTrainingInputModel input);

        Task DeleteAsync(int id);
    }
}
