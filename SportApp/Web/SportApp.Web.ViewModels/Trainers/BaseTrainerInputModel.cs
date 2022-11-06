namespace SportApp.Web.ViewModels.Trainers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseTrainerInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(30)]
        [MaxLength(500)]
        public string InfoCard { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public decimal PricePerTraining { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoryItems { get; set; }
    }
}
