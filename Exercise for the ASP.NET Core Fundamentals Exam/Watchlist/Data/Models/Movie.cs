﻿using System.ComponentModel.DataAnnotations;

namespace Watchlist.Data.Models
{
    public class Movie
    {
        public Movie()
        {
            this.UsersMovies = new HashSet<UserMovie>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Director { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Required]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }

        public Genre? Genre { get; set; }

        public ICollection<UserMovie>? UsersMovies { get; set; }
    }
}
