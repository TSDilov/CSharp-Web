using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Data.Models;
using Watchlist.Models;
using Watchlist.Services;

namespace Watchlist.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public async Task<IActionResult> All()
        {
            var model = await movieService.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var addModel = new AddMovieViewModel()
            {
                Genres = await this.movieService.GetGenresAsync(),
            };

            return View(addModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }

            try
            {
                await this.movieService.AddMovieAsync(addModel);
                return RedirectToAction("All");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "You don't add a movie!!");

                return View(addModel);
            }
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            try
            {
                var userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                await this.movieService.AddMovieToCollectionAsync(movieId, userId);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Watched()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await this.movieService.GetWatchedAsync(userId);

            return View("Watched", model);
        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await movieService.RemoveMovieFromCollectionAsync(movieId, userId);

            return RedirectToAction("Watched");
        }
    }
}
