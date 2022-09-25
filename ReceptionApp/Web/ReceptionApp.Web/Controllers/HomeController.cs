namespace ReceptionApp.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ReceptionApp.Data;
    using ReceptionApp.Data.Common.Repositories;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services;
    using ReceptionApp.Services.Data;
    using ReceptionApp.Web.ViewModels;
    using ReceptionApp.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;
        private readonly IRecipeTrackerService recipeTrackerService;

        public HomeController(IGetCountsService countsService, IRecipeTrackerService recipeTrackerService)
        {
            this.countsService = countsService;
            this.recipeTrackerService = recipeTrackerService;
        }

        public IActionResult Index()
        {
            var viewModel = this.countsService.GetCounts();
            return this.View(viewModel);
        }

        public IActionResult AddFromGotvq()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFromGotvq(AddFromGotvqViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.recipeTrackerService.PopulateDBwithRecipe(model.recipeUrl);
            return this.Redirect("/");
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
