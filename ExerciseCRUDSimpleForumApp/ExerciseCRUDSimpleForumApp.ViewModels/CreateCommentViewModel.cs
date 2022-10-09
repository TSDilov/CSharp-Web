using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.ViewModels
{
    public class CreateCommentViewModel
    {

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string? Content { get; set; }

        public int PostId { get; set; }

        public string? AddedByUserId { get; set; }

        public string? Username { get; set; }
    }
}
