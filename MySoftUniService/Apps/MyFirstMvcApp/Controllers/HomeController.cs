
using Server.HTTP;
using Server.MvcFramework;
using System.Linq;
using System.Text;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponce Index(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponce About(HttpRequest request)
        {
            var responceHtml = "<h1>About...!</h1>";
            var responceBodyBytes = Encoding.UTF8.GetBytes(responceHtml);
            var responce = new HttpResponce("text/html", responceBodyBytes);

            return responce;
        }
    }
}
