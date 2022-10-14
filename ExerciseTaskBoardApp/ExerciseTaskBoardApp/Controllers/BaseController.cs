using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTaskBoardApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
