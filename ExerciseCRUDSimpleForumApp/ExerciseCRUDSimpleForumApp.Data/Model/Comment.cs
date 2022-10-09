using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.Data.Model
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string? Content { get; set; }

        public int PostId { get; set; }

        public Post? Post { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? AddedByUserId { get; set; }

        public IdentityUser? AddedByUser { get; set; }

        public string? Username { get; set; }
    }
}
