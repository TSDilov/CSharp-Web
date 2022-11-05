namespace SportApp.Data.Models
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using SportApp.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Trainer : BaseDeletableModel<int>
    {
        public Trainer()
        {
            this.ApplicationUsersTrainers = new HashSet<ApplicationUserTrainer>();
            this.Categories = new HashSet<Category>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string InfoCard { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Image")]
        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<ApplicationUserTrainer> ApplicationUsersTrainers { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
