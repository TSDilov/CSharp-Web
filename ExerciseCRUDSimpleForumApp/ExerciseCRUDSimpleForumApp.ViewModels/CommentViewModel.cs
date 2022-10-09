using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public int PostId { get; set; }

        public string? AddedByUserId { get; set; }

        public string? Username { get; set; }
    }
}
