using CooperativeFinancing.Models;
using CooperativeFinancing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Admin.Controllers
{
    [Area("Admin")] // Ensure the controller belongs to the "Admin" area
    public class UserController : Controller
    {
        private readonly CooperativeContext _context;

        public UserController(CooperativeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult UserTable()
        {
            var users = _context.UserMembersView // ✅ Fetch data from the view
            .Select(u => new LoginDetailsView
            {
                User_Id = u.User_Id,
                Member_Id = u.Member_Id,
                Member_Name = u.Member_Name, // ✅ Full name from the View
                Username = u.Username,
                Password = u.Password, // ⚠️ Consider not sending this to the frontend
                Is_Admin = u.Is_Admin
            }).ToList();

            return View(users);
        }
    }
}
