using Server.HTTP;
using Server.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class CarsController : Controller
    {
        public HttpResponce All(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponce Add(HttpRequest request)
        {
            return this.View();
        }
    }
}
