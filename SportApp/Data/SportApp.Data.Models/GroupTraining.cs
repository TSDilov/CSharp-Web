namespace SportApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SportApp.Data.Common.Models;

    public class GroupTraining : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Place { get; set; }

        [Required]
        public string DaysOfWeek { get; set; }

        [Required]
        public TimeSpan StartHour { get; set; }

        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; }
    }
}
