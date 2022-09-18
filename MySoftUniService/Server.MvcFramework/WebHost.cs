

using Server.HTTP;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.MvcFramework
{
    public class WebHost
    {
        public static async Task RunAsync(IMvcApplication application, int port = 80)
        {
            List<Route> routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);

            IHttpServer server = new HttpServer();

            foreach (var route in routeTable)
            {
                server.AddRoute(route.Path, route.Action);
            }

            await server.StartAsync(port);
        }
    }
}
