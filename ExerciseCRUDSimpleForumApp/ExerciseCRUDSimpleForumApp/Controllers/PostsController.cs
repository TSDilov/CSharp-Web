﻿using ExerciseCRUDSimpleForumApp.Data.Model;
using ExerciseCRUDSimpleForumApp.Service;
using ExerciseCRUDSimpleForumApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseCRUDSimpleForumApp.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult All()
        {
            var posts = new List<PostViewModel>();
            var postsFromBase = this.postService.GetAll();

            foreach (var post in postsFromBase)
            {
                var postForView = new PostViewModel 
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                };

                posts.Add(postForView);
            }
            return View(posts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            await this.postService.CreateAsync(model);
            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var postFromBase = this.postService.GetById(id);
            return View(new CreatePostViewModel() 
            {
                Title = postFromBase.Title,
                Content = postFromBase.Content,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            await this.postService.EditAsync(model, id);
            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.postService.DeleteAsync(id);
            return RedirectToAction("All");
        }
    }
}
