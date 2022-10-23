using Library.Services;
using Library.View_Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.bookService.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var addModel = new AddBookViewModel()
            {
                Categories = await this.bookService.GetCategoryAsync(),
            };

            return View(addModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addModel);
            }

            try
            {
                await this.bookService.AddBookAsync(addModel);
                return RedirectToAction("All");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "You don't add a book!!");

                return View(addModel);
            }
        }

        public async Task<IActionResult> AddToCollection(int bookId)
        {
            try
            {
                var userId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                await this.bookService.AddBookToCollectionAsync(bookId, userId);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await this.bookService.GetMineAsync(userId);

            return View("Mine", model);
        }

        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await this.bookService.RemoveMovieFromCollectionAsync(bookId, userId);

            return RedirectToAction("Mine");
        }
    }
}
