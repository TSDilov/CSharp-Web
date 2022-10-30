namespace ReceptionApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IIngredientsService
    {
        Task<IEnumerable<T>> GetALlAsync<T>();
    }
}
