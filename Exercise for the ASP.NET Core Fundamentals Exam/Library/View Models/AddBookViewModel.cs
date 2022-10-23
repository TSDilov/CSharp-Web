using Library.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.View_Models
{
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Categories = new List<Category>();
        }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Author { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "10.0", ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
