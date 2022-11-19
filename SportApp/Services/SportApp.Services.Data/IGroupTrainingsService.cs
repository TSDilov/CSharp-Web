namespace SportApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SportApp.Web.ViewModels.GroupTrainings;

    public interface IGroupTrainingsService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task CreateAsync(CreateGroupTrainingInputModel input);
    }
}
