using ExerciseCRUDSimpleForumApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.Service
{
    public interface ICommentService
    {
        Task CreateAsync(CreateCommentViewModel model);
    }
}
