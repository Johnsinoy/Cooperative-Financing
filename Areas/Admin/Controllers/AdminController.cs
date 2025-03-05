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

        [HttpGet]
        public IActionResult MemberList()
        {
            var members = _context.CooperativeMembers.ToList();
            return View(members);
        }

        [HttpGet]
        public IActionResult SearchMember(string FirstName, string LastName, string City, string Province, string Street, string PhoneNumber, string Email)
        {
            var members = _context.CooperativeMembers.AsQueryable();

            if (!string.IsNullOrEmpty(FirstName))
                members = members.Where(m => m.FirstName.Contains(FirstName));

            if (!string.IsNullOrEmpty(LastName))
                members = members.Where(m => m.LastName.Contains(LastName));

            if (!string.IsNullOrEmpty(City))
                members = members.Where(m => m.City.Contains(City));

            if (!string.IsNullOrEmpty(Province))
                members = members.Where(m => m.Province.Contains(Province));

            if (!string.IsNullOrEmpty(Street))
                members = members.Where(m => m.Street.Contains(Street));

            if (!string.IsNullOrEmpty(PhoneNumber))
                members = members.Where(m => m.Phone.Contains(PhoneNumber));

            if (!string.IsNullOrEmpty(Email))
                members = members.Where(m => m.Email.Contains(Email));

            return PartialView("_MemberTable", members.ToList());
        }

        [HttpPost]
        public IActionResult DeleteMember(int id)
        {
            var member = _context.CooperativeMembers.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.CooperativeMembers.Remove(member);
            _context.SaveChanges();
            return Json(new { success = true });
        }

    }
}
