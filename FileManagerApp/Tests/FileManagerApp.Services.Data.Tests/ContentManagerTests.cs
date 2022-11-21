namespace FileManagerApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FileManagerApp.Data.Common.Repositories;
    using FileManagerApp.Data.Models;
    using FileManagerApp.Web.ViewModels;
    using Moq;
    using Xunit;

    public class ContentManagerTests
    {
        [Fact]
        public async Task StoreFile()
        {
            var list = new List<StreamInfoBaseModel>();
            var mockRepo = new Mock<IRepository<StreamInfoBaseModel>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<StreamInfoBaseModel>()))
                .Callback((StreamInfoBaseModel stream) => list.Add(stream));
            var manager = new ContentManager(mockRepo.Object);

            var file = new StreamInfoViewModel
            {
                Id = "944a168d-cd57-4dbd-9218-00da74446440",
                FileName = "Test",
                Description = "Testing description",
            };

            var operationSuccessForAdd = await manager.StoreAsync(file);

            Assert.True(operationSuccessForAdd.Success);
        }
    }
}
