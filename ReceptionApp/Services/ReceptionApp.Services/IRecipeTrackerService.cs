namespace ReceptionApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ReceptionApp.Services.Models;

    public interface IRecipeTrackerService
    {
        Task PopulateDBwithRecipe(string utl);
    }
}
