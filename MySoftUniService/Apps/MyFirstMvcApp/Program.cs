using MyFirstMvcApp.Controllers;
using Server.HTTP;
using Server.MvcFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await WebHost.RunAsync(new StartUp());
        }
    }
}
