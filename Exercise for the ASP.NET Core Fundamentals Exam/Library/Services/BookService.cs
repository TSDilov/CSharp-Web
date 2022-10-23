using Library.Data;
using Library.Data.Models;
using Library.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            var book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };

            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await this.dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid Book ID");
            }

            var book = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (!user.ApplicationUsersBooks.Any(b => b.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    BookId = bookId,
                    ApplicationUserId = user.Id,
                });

                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            var books = await this.dbContext.Books
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                }).ToListAsync();

            return books;
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await this.dbContext.Categories.ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetMineAsync(string userId)
        {
            var user = await this.dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(b => b.Book)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.ApplicationUsersBooks
                .Select(m => new BookViewModel()
                {
                    Author = m.Book.Author,
                    Category = m.Book.Category?.Name,
                    Id = m.BookId,
                    ImageUrl = m.Book.ImageUrl,
                    Title = m.Book.Title,
                    Rating = m.Book.Rating,
                    Description = m.Book.Description,
                });
        }

        public async Task RemoveMovieFromCollectionAsync(int bookId, string userId)
        {
            var user = await this.dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = user.ApplicationUsersBooks.FirstOrDefault(b => b.BookId == bookId);

            if (book != null)
            {
                user.ApplicationUsersBooks.Remove(book);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
