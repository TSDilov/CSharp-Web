namespace SportApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TrainerCategory
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; }
    }
}
