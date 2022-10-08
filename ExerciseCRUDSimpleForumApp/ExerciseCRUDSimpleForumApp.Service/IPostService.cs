using ExerciseCRUDSimpleForumApp.Data.Model;
using ExerciseCRUDSimpleForumApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.Service
{
    public interface IPostService
    {
        IEnumerable<PostViewModel> GetAll();

        Task CreateAsync(CreatePostViewModel post);

        Post GetById(int id);

        Task EditAsync(CreatePostViewModel post, int id);

        Task DeleteAsync(int id);
    }
}
