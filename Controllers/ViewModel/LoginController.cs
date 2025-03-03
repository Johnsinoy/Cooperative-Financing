using CooperativeFinancing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Controllers.ViewModel
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("UserLoggedIn") == "true")
            {
                return RedirectToAction("Index", "Home"); // ✅ Redirect to home if logged in
            }

            return View(new LoginViewModel()); // ✅ Ensure it loads Views/Login/Index.cshtml

        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (model == null) model = new LoginViewModel(); // Prevent null reference
            if (model.Username == "admin" && model.Password == "admin")
            {
                HttpContext.Session.SetString("UserLoggedIn", "true"); // ✅ Store session on login
                                                                     
                return RedirectToAction("Index", "Admin", new { area = "Admin" }); // ✅ Redirect to the Admin area
            }
            model.errorMessage = "Invalid username or password";
            return View(model);
        }
    }
}
