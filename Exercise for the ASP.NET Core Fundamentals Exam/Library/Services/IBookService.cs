using Library.Data.Models;
using Library.View_Models;

namespace Library.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<Category>> GetCategoryAsync();

        Task AddBookAsync(AddBookViewModel model);

        Task AddBookToCollectionAsync(int bookId, string userId);

        Task<IEnumerable<BookViewModel>> GetMineAsync(string userId);

        Task RemoveMovieFromCollectionAsync(int bookId, string userId);
    }
}
