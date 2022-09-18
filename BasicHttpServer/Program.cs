using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasicHttpServer
{
    internal class Program
    {
        static Dictionary<string, int> SessionStorage = new Dictionary<string, int>();
        const string NewLine = "\r\n";
        static async Task Main(string[] args)
        {          
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                ProcessClientAsync(client);
            }
        }

        public static async Task ProcessClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1000000];
                var lenght = await stream.ReadAsync(buffer, 0, buffer.Length);

                var requestString = Encoding.UTF8.GetString(buffer, 0, lenght);
                Console.WriteLine(requestString);

                var sid = Guid.NewGuid().ToString();
                var match = Regex.Match(requestString, @"sid=[^\n]*\r\n");
                if (match.Success)
                {
                    sid = match.Value.Substring(4);
                }

                if (!SessionStorage.ContainsKey(sid))
                {
                    SessionStorage.Add(sid, 0);
                }

                SessionStorage[sid]++;

                var html = $"<h1>Hello from testing server + {DateTime.Now} for the {SessionStorage[sid]} time!</h1>";
                var responce = "HTTP/1.1 200 OK" + NewLine +
                    "Server: TestServer 2020" + NewLine +
                    "Content-Type: text/html; charset=utf-8" + NewLine +
                    $"Set-Cookie: sid={sid}; HttpOnly; Max-Age" + (10 * 24 * 60 * 60) + NewLine +
                    "Content-Lenght: " + html.Length + NewLine +
                    NewLine +
                    html + NewLine;

                byte[] responseBytes = Encoding.UTF8.GetBytes(responce);
                await stream.WriteAsync(responseBytes);

                Console.WriteLine(new string('-', 60));
            }
        }

        public static async Task ReadData()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var url = "https://softuni.bg/modules/108/csharp-web/1365";
            HttpClient httpClient = new HttpClient();
            var responce = await httpClient.GetAsync(url);
            Console.WriteLine(responce.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine, responce.Headers.Select(x => x.Key + ": " + x.Value.First())));
        }
    }
}
