using Microsoft.AspNetCore.Mvc;
using CooperativeFinancing.Models;
using System;
using System.Threading.Tasks;

namespace CooperativeFinancing.Areas.Admin.Controllers
{
    [Area("Admin")] // Ensures the controller belongs to the Admin area
    public class AdminController : Controller
    {
        private readonly CooperativeContext _context;

        public AdminController(CooperativeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMemberPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemberPage([FromForm] CooperativeMembers member)
        {
            Console.WriteLine("🟢 CreateMember method triggered!");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("🔴 Model state is invalid.");
                return View("AddMemberPage", member);
            }

            try
            {
                Console.WriteLine($"🟢 Adding Member: {member.FirstName} {member.LastName}");

                _context.CooperativeMembers.Add(member);
                await _context.SaveChangesAsync();

                Console.WriteLine("🟢 Member successfully added.");
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 SERVER ERROR: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the member.");
                return View("AddMemberPage", member);
            }
        }
    }
}
