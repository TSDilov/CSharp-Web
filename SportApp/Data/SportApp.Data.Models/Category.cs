using SportApp.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportApp.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Trainers = new HashSet<Trainer>();
        }

        public string Name { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}
