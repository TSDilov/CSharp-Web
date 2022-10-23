using Microsoft.Build.Framework;

namespace Watchlist.Data.Models
{
    public class UserMovie
    {
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}