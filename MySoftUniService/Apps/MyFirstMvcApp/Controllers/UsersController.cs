
using Server.HTTP;
using Server.MvcFramework;
using System.IO;
using System.Text;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponce Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponce Register(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponce AfterLogin(HttpRequest request)
        {

            return this.Redirect("/");
        }
    }
}
