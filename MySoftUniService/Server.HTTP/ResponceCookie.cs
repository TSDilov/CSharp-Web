using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.HTTP
{
    public class ResponceCookie : Cookie
    {
        public ResponceCookie(string name, string value)
            : base(name, value)
        {
            this.Path = "/";
        }

        public int MaxAge { get; set; }
        public bool HttpOnly { get; set; }
        public string Path { get; set; }
        public bool Secure { get; set; }

        public override string ToString()
        {
            var cookieBuilder = new StringBuilder();
            cookieBuilder.Append($"{this.Name}={this.Value}; Path={this.Path};");
            if (this.MaxAge != 0)
            {
                cookieBuilder.Append($" Max-Age={this.MaxAge};");
            }

            if (this.HttpOnly)
            {
                cookieBuilder.Append(" HttpOnly;");
            }

            if (this.Secure)
            {
                cookieBuilder.Append(" Secure;");
            }

            return cookieBuilder.ToString();
        }        
    }
}
