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
        private readonly IRecipeService recipeService;

        public HomeController(
            IGetCountsService countsService,
            IRecipeService recipeService)
        {
            this.countsService = countsService;
            this.recipeService = recipeService;
        }

        public IActionResult Index()
        {
            var viewModel = this.countsService.GetCounts();
            return this.View(viewModel);
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
