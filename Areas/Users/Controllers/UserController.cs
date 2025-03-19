using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("Users/[controller]")]
    public class UserController : Controller
    {
        [Route("")] // ✅ Ensures "/Users/User" works
        [Route("Index")] // ✅ Allows "/Users/User/Index"
        public IActionResult Index()
        {
            ViewData["IndexUrl"] = Url.Action("Index", "User", new { area = "Users" });
            return View();
        }
    }
}
