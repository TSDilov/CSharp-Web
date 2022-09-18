using Server.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MvcFramework
{
    public class Route
    {
        public Route(string path, HttpMethod method, Func<HttpRequest,HttpResponce> action)
        {
            this.Path = path;
            this.Method = method;
            this.Action = action;
        }
        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public Func<HttpRequest, HttpResponce> Action { get; set; }
    }
}
