using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = requestString.Split(new string[] { HttpConstants.NewLine },
                StringSplitOptions.None);
            var headerLine = lines[0];
            var headerParts = headerLine.Split(" ");
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerParts[0], true);
            this.Path = headerParts[1];

            var lineIndex = 1;
            var isInHeaders = true;
            var bodyBuilder = new StringBuilder();

            while (lineIndex < lines.Length)
            {
                var currentLine = lines[lineIndex];
                lineIndex++;
                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    isInHeaders = false;
                    continue;
                }

                if (isInHeaders)
                {
                    this.Headers.Add(new Header(currentLine));
                }
                else
                {
                    bodyBuilder.AppendLine(currentLine);
                } 
            }

            if (this.Headers.Any(x => x.Name == HttpConstants.RequestCookieHeader))
            {
                var cookiesAsString = this.Headers
                    .FirstOrDefault(x => x.Name == HttpConstants.RequestCookieHeader).Value;
                var cookies = cookiesAsString.Split(new string[] { "; " },
                    StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookieAsString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookieAsString));
                }
            }

            this.Body = bodyBuilder.ToString();
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public List<Header> Headers { get; set; }
        public List<Cookie> Cookies { get; set; }
        public string Body { get; set; }
    }
}
