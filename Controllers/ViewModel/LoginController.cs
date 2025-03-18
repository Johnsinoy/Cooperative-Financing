using CooperativeFinancing.Models;
using CooperativeFinancing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CooperativeFinancing.Controllers.ViewModel
{
    public class LoginController : Controller
    {
        private readonly CooperativeContext _context;

        // ✅ Inject Database Context
        public LoginController(CooperativeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // ✅ Redirect if already logged in
            if (HttpContext.Session.GetString("UserLoggedIn") == "true")
            {
                string userRole = HttpContext.Session.GetString("UserRole");
                string area = userRole == "Admin" ? "Admin" : "Users";
                string controller = userRole == "Admin" ? "Admin" : "User";

                return RedirectToAction("Index", controller, new { area });
            }

            return View(new LoginViewModel()); // ✅ Load login page
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (model == null) model = new LoginViewModel(); // ✅ Prevent null reference

            // ✅ Query the database for the user
            var user = _context.CooperativeUsers
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null) // ✅ User exists in the database
            {
                // ✅ Determine user role (Admin or User)
                string userRole = user.Is_Admin ? "Admin" : "User";

                // ✅ Store session data
                HttpContext.Session.SetString("UserLoggedIn", "true");
                HttpContext.Session.SetString("UserRole", userRole);
                HttpContext.Session.SetString("Username", user.Username);

                // ✅ Redirect to dashboard based on role
                return RedirectToAction("Index", userRole, new { area = userRole == "Admin" ? "Admin" : "Users" });
            }

            // ❌ Login failed
            model.errorMessage = "Invalid username or password";
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // ✅ Clear session
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
