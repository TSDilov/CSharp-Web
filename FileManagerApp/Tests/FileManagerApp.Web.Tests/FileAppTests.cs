namespace FileManagerApp.Web.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using FileManagerApp.Data;
    using FileManagerApp.Data.Common.Repositories;
    using FileManagerApp.Data.Models;
    using FileManagerApp.Services;
    using FileManagerApp.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FileAppTests
    {
        [Fact]
        public async Task FilePage()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            HttpClient client = webApplicationFactory.CreateClient();

            var responce = await client.GetAsync("/File/Index");
            responce.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task FilePageShouldContainsAddButton()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            HttpClient client = webApplicationFactory.CreateClient();

            var responce = await client.GetAsync("/File/Index");
            var html = await responce.Content.ReadAsStringAsync();

            Assert.Contains("<a class=\"btn btn-primary\" href=\"/File/Create\"> Add file</a>", html);
        }
    }
}
