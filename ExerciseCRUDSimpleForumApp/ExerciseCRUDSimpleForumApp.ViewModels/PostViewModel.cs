using System.ComponentModel.DataAnnotations;

namespace ExerciseCRUDSimpleForumApp.ViewModels
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            this.Comments = new List<CommentViewModel>();
        }
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? AddedByUserId { get; set; }

        public string? UserName { get; set; }

        public ICollection<CommentViewModel>? Comments { get; set; }
    }
}
