﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.Data.Model
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Content { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? AddedByUserId { get; set; }

        public IdentityUser? AddedByUser { get; set; }

        public string? Username { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
