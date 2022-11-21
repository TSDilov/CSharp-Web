namespace FileManagerApp.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using FileManagerApp.Common;
    using FileManagerApp.Services;
    using FileManagerApp.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OneBitSoftware.Utilities;

    public class FileController : Controller
    {
        private readonly IContentManager fileService;

        public FileController(IContentManager fileService)
        {
            this.fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var files = await this.fileService.GetAll<StreamInfoViewModel>();
            return this.View(files);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new StreamInfoViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(IFormFile file, StreamInfoViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            input.Length = file.Length;
            input.FileName = file.FileName;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                byte[] data = stream.ToArray();
                input.Content = data;
            }

            var operationResult = await this.fileService.StoreAsync(input);
            if (operationResult.Success)
            {
                return this.Redirect("/");
            }

            return this.View(input);
        }

        [Authorize]
        public async Task<IActionResult> Details(string fileId)
        {
            var exist = await this.fileService.ExistsAsync(fileId);
            if (!exist)
            {
                return this.NotFound();
            }

            var file = await this.fileService.GetAsync<StreamInfoViewModel>(fileId);
            return this.View(file);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string fileId)
        {
            var model = await this.fileService.GetAsync<StreamInfoViewModel>(fileId);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(string fileId, StreamInfoViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var operationResult = await this.fileService.UpdateAsync(fileId, model);
            if (operationResult.Success)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string fileId)
        {
            await this.fileService.DeleteAsync(fileId);
            return this.RedirectToAction("Index");
        }
    }
}
