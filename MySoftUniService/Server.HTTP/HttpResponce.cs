﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.HTTP
{
    public class HttpResponce
    {
        public HttpResponce(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
        }
        public HttpResponce(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.StatusCode = statusCode;
            this.Body = body;
            this.Headers = new List<Header>
            {
                { new Header("Content-Type", contentType) },
                { new Header("Content-Length", this.Body.Length.ToString())},
            };
            this.Cookies = new List<Cookie>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public byte[] Body { get; set; }

        public override string ToString()
        {
            var responceBuilder = new StringBuilder();
            responceBuilder.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + HttpConstants.NewLine);
            foreach (var header in this.Headers)
            { 
                responceBuilder.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                responceBuilder.Append("Set-Cookie: " + cookie.ToString() + HttpConstants.NewLine);
            }

            responceBuilder.Append(HttpConstants.NewLine);
            return responceBuilder.ToString();
        }

    }
}
