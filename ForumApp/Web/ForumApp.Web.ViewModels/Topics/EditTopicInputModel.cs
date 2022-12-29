namespace ForumApp.Web.ViewModels.Topics
{
    using System.ComponentModel.DataAnnotations;
    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;

    public class EditTopicInputModel : IMapFrom<Topic>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
