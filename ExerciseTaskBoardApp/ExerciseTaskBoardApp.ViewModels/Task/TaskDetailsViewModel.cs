using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseTaskBoardApp.ViewModels.Task
{
    public class TaskDetailsViewModel : TaskViewModel
    {
        public string? CreatedOn { get; set; }

        public string? Board { get; set; }
    }
}
