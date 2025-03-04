using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Users.Controllers
{
    [Area("Users")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
