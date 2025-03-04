using CooperativeFinancing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CooperativeFinancing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly CooperativeContext _context;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMemberPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMember(CooperativeMembers model)
        {
            if (ModelState.IsValid)
            {
                _context.CooperativeMembers.Add(model);  // Add new member to the database
                _context.SaveChanges();       // Commit changes to the database

                TempData["SuccessMessage"] = "Member added successfully!";
                return RedirectToAction("Index"); // Redirect back to the dashboard
            }

            return View("AddMemberPage", model); // Reload AddMemberPage if validation fails
        }

    }
}
