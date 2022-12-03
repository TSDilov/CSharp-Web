namespace SportApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using SportApp.Data.Common.Repositories;
    using SportApp.Data.Models;
    using SportApp.Web.ViewModels.Administration.Categories;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public async Task CreateCategoryAndGetAllCategories()
        {
            var list = new List<Category>();
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>()))
                .Callback((Category category) => list.Add(category));
            var service = new CategoriesService(mockRepo.Object);

            var category = new CategoryInputModel
            {
                Name = "Test",
                CreatedOn = DateTime.Now,
            };

            await service.CreateAsync(category);

            var categories = service.All();

            Assert.Equal(1, list.Count);
            Assert.Contains(categories, x => x.Name == "Test");
            Assert.DoesNotContain(categories, x => x.Name == "NotTest");
        }

        [Fact]
        public async Task DeleteCategory()
        {
            var list = new List<Category>();
            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new CategoriesService(mockRepo.Object);

            var category = new Category
            {
                Id = 1,
                Name = "Test",
                CreatedOn = DateTime.Now,
            };

            list.Add(category);

            await service.DeleteAsync(1);

            Assert.Equal(0, list.Count);
        }
    }
}
