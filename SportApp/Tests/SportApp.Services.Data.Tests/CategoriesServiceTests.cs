namespace SportApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
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
        private readonly ICategoriesService service;
        private readonly Mock<IDeletableEntityRepository<Category>> categoryRepo = new Mock<IDeletableEntityRepository<Category>>();
        private readonly List<Category> categories;

        public CategoriesServiceTests()
        {
            this.service = new CategoriesService(this.categoryRepo.Object);
            this.categories = new List<Category>();
        }

        [Fact]
        public async Task CreateCategoryAndGetAllCategories()
        {
            this.categoryRepo.Setup(x => x.All()).Returns(this.categories.AsQueryable());
            this.categoryRepo.Setup(x => x.AddAsync(It.IsAny<Category>()))
                .Callback((Category category) => this.categories.Add(category));

            var category = new CategoryInputModel
            {
                Name = "Test",
                CreatedOn = DateTime.Now,
            };

            await this.service.CreateAsync(category);

            var categories = this.service.All();

            Assert.Equal(1, this.categories.Count);
            Assert.Contains(categories, x => x.Name == "Test");
            Assert.DoesNotContain(categories, x => x.Name == "NotTest");
        }

        [Fact]
        public async Task DeleteCategory()
        {

        }
    }
}
