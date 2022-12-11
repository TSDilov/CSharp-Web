namespace SportApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SportApp.Data;
    using SportApp.Data.Common.Repositories;
    using SportApp.Data.Models;
    using SportApp.Data.Repositories;
    using SportApp.Web.ViewModels.Administration.Categories;
    using SportApp.Web.ViewModels.Comment;
    using Xunit;

    public class CommentServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Comment>> commentRepo = new Mock<IDeletableEntityRepository<Comment>>();
        private readonly List<Category> categories;
        private ICommentsService service;
        private ApplicationDbContext applicationDbContext;

        public CommentServiceTests()
        {
            this.categories = new List<Category>();
        }

        [Fact]
        public async Task CreateCommentAndGetAllComments()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SportDb").Options;
            this.applicationDbContext = new ApplicationDbContext(contextOptions);
            var commentRepo = new EfDeletableEntityRepository<Comment>(this.applicationDbContext);
            this.service = new CommentsServise(commentRepo);

            var comment = new CommentInputModel
            {
                TrainerId = 1,
                Name = "Test",
                Email = "test@abv.bg",
                Subject = "Test Subject",
                Message = "Test Message",
            };

            await this.service.CreateAsync(comment, "user");

            Assert.Equal(1, commentRepo.All().ToList().Count);
            Assert.Contains(commentRepo.All().ToList(), x => x.Name == "Test");
        }

        [Fact]
        public async Task DeleteComment()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SportDb").Options;
            this.applicationDbContext = new ApplicationDbContext(contextOptions);
            var commentRepo = new EfDeletableEntityRepository<Comment>(this.applicationDbContext);
            this.service = new CommentsServise(commentRepo);

            var comment = new Comment
            {
                Id = 1,
                Name = "Test",
                Email = "test@abv.bg",
                Subject = "Test Subject",
                Message = "Test Message",
            };

            await commentRepo.AddAsync(comment);
            await commentRepo.SaveChangesAsync();

            await this.service.DeleteAsync(1);

            Assert.Equal(0, commentRepo.All().ToList().Count);
            Assert.DoesNotContain(commentRepo.All().ToList(), x => x.Name == "Test");
            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetCommentById()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SportDb").Options;
            this.applicationDbContext = new ApplicationDbContext(contextOptions);
            var commentRepo = new EfDeletableEntityRepository<Comment>(this.applicationDbContext);
            this.service = new CommentsServise(commentRepo);

            var comment = new Comment
            {
                Id = 1,
                Name = "Test",
                Email = "test@abv.bg",
                Subject = "Test Subject",
                Message = "Test Message",
            };

            await commentRepo.AddAsync(comment);
            await commentRepo.SaveChangesAsync();

            var gettedComment = this.service.GetById(comment.Id);

            Assert.Equal("test@abv.bg", gettedComment.Email);
            Assert.Contains(commentRepo.All().ToList(), x => x.Name == "Test");
            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetTrainerComments()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("SportDb").Options;
            this.applicationDbContext = new ApplicationDbContext(contextOptions);
            var commentRepo = new EfDeletableEntityRepository<Comment>(this.applicationDbContext);
            this.service = new CommentsServise(commentRepo);

            var comment = new Comment
            {
                Id = 1,
                Name = "Test",
                Email = "test@abv.bg",
                Subject = "Test Subject",
                Message = "Test Message",
                ApplicationUserId = "user",
                TrainerId = 2,
            };

            await commentRepo.AddAsync(comment);
            await commentRepo.SaveChangesAsync();

            var comment2 = new Comment
            {
                Id = 2,
                Name = "Test2",
                Email = "test@abv.bg2",
                Subject = "Test Subject2",
                Message = "Test Message2",
                ApplicationUserId = "user2",
                TrainerId = 1,
            };

            await commentRepo.AddAsync(comment2);
            await commentRepo.SaveChangesAsync();

            var getTrainerComments = this.service.GetTrainerComments(comment.TrainerId);

            Assert.Equal(1, getTrainerComments.Count());
            this.applicationDbContext.Database.EnsureDeleted();
            this.applicationDbContext.Database.EnsureCreated();
        }
    }
}
