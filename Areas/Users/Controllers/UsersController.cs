using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Users.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
