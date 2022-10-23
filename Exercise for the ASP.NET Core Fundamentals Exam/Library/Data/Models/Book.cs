using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class Book
    {
        public Book()
        {
            this.ApplicationUsersBooks = new HashSet<ApplicationUserBook>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Author { get; set; }

        [Required]
        [MaxLength(5000)]
        public string? Description { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Required]
        public decimal Rating { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<ApplicationUserBook>? ApplicationUsersBooks { get; set; }
    }
}
