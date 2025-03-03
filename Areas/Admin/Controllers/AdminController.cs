ausing Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
