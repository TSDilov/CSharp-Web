namespace ForumApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ForumApp.Data.Common.Models;

    public class Topic : BaseDeletableModel<string>
    {
        public Topic()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Replies = new HashSet<Reply>();
            this.Likes = new HashSet<Like>();
            this.Awards = new HashSet<Award>();
        }

        [Required]
        public string Content { get; set; }

        public string TopicUserId { get; set; }

        public ICollection<Reply> Replies { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Award> Awards { get; set; }
    }
}
