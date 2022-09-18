using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.HTTP
{
    public class HttpServer : IHttpServer
    {
        IDictionary<string, Func<HttpRequest, HttpResponce>> routeTable = 
            new Dictionary<string, Func<HttpRequest, HttpResponce>>();

        public void AddRoute(string path, Func<HttpRequest, HttpResponce> action)
        {
            if (routeTable.ContainsKey(path))
            {
                routeTable[path] = action;
            }
            else 
            {
                routeTable.Add(path, action);
            }           
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using (var stream = tcpClient.GetStream())
                {
                    List<byte> data = new List<byte>();
                    var position = 0;
                    byte[] buffer = new byte[4096];
                    while (true)
                    {
                        var count = await stream.ReadAsync(buffer, position, buffer.Length);
                        position += count;
                        if (count < buffer.Length)
                        {
                            var partialbuffer = new byte[count];
                            Array.Copy(buffer, partialbuffer, count);
                            data.AddRange(partialbuffer);
                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }
                    }

                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine(requestAsString);

                    HttpResponce responce;
                    if (this.routeTable.ContainsKey(request.Path))
                    {
                        var action = this.routeTable[request.Path];
                        responce = action(request);
                    }
                    else 
                    {
                        responce = new HttpResponce("text/html", new byte[0], HttpStatusCode.NotFound);
                    }

                    responce.Headers.Add(new Header("Server", "My Server 1.0"));
                    responce.Cookies.Add(new ResponceCookie("sid", Guid.NewGuid().ToString())
                    {
                        HttpOnly = true,
                        MaxAge = 50 * 24 * 60 * 60,
                    });
                    var responceHeaderBytes = Encoding.UTF8.GetBytes(responce.ToString());
                    await stream.WriteAsync(responceHeaderBytes, 0, responceHeaderBytes.Length);
                    await stream.WriteAsync(responce.Body, 0, responce.Body.Length);
                }

                tcpClient.Close();
            }
            catch (Exception)
            {
                throw;
            }           
        }
    }
}
