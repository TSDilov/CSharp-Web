using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MvcFramework
{
    public interface IMvcApplication
    {
        void ConfigureServices();
        void Configure(List<Route> routeTable);
    }
}
