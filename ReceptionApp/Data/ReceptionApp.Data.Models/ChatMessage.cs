namespace ReceptionApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ReceptionApp.Data.Common.Models;

    public class ChatMessage : BaseDeletableModel<int>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
