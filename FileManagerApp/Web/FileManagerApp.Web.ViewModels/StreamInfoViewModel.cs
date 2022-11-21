namespace FileManagerApp.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FileManagerApp.Data.Models;
    using FileManagerApp.Services.Mapping;

    public class StreamInfoViewModel : IMapFrom<StreamInfoBaseModel>
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Description { get; set; }

        public long Length { get; set; }

        public byte[] Content { get; set; }
    }
}
