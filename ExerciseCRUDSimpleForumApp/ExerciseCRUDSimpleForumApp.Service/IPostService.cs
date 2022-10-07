using ExerciseCRUDSimpleForumApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.Service
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
    }
}
