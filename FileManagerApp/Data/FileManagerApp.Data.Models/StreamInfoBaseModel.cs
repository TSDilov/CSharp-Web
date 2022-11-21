namespace FileManagerApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FileManagerApp.Data.Common.Models;

    public class StreamInfoBaseModel : BaseModel<string>
    {
        public StreamInfoBaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FileName { get; set; }

        public string Description { get; set; }

        public long Length { get; set; }

        public byte[] Content { get; set; }
    }
}
