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

        [HttpPost]
        public IActionResult UpdateMember([FromBody] CooperativeMembers updatedMember)
        {
            Console.WriteLine("🟢 UpdateMember method triggered!"); // Debugging Step

            var member = _context.CooperativeMembers.Find(updatedMember.Member_Id);

            if (member == null)
            {
                Console.WriteLine("🔴 Member not found in database.");
                return Json(new { success = false, message = "Member not found." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, message = "Validation failed.", errors });
            }

            Console.WriteLine($"🟢 Updating Member: {member.FirstName} {member.LastName}");

            // Update fields
            member.FirstName = updatedMember.FirstName;
            member.LastName = updatedMember.LastName;
            member.Street = updatedMember.Street;
            member.City = updatedMember.City;
            member.Province = updatedMember.Province;
            member.Email = updatedMember.Email;
            member.Phone = updatedMember.Phone;
            member.JoinDate = updatedMember.JoinDate;
            member.Contribution = updatedMember.Contribution;

            try
            {
                int changes = _context.SaveChanges(); // Capture number of changes
                if (changes > 0)
                {
                    Console.WriteLine("🟢 Member successfully updated.");
                    return Json(new { success = true, message = "Member updated successfully!" });
                }
                else
                {
                    Console.WriteLine("🔴 No changes detected in database.");
                    return Json(new { success = false, message = "No changes detected." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 SERVER ERROR: {ex.Message}");
                return Json(new { success = false, message = "An error occurred.", error = ex.Message });
            }
        } 

        [HttpGet]
        public IActionResult GetMember(int id)
        {
            var member = _context.CooperativeMembers.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            return Json(new
            {
                memberID = member.Member_Id,
                firstName = member.FirstName,
                lastName = member.LastName,
                street = member.Street,
                city = member.City,
                province = member.Province,
                email = member.Email,
                phone = member.Phone,
                joinDate = member.JoinDate.ToString("yyyy-MM-dd"),
                contribution = member.Contribution
            });
        }

        [HttpGet]
        public IActionResult AddLoanPage()
        {
            var members = _context.CooperativeMembers.ToList(); // Fetch all members

            if (members == null || !members.Any())
            {
                ViewBag.Members = new List<CooperativeMembers>(); // Ensure it's not null
            }
            else
            {
                ViewBag.Members = members;
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLoanPage([FromForm] CooperativeLoans loan)
        {
            Console.WriteLine("🟢 AddLoanPage (POST) method triggered!");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("🔴 Model state is invalid.");
                var members = _context.CooperativeMembers.ToList(); // Fetch members again for dropdown
                ViewBag.Members = members;
                return View("AddLoanPage", loan); // Return the view with validation errors
            }

            try
            {
                Console.WriteLine($"🟢 Adding Loan for Member ID: {loan.Member_Id}");

                _context.CooperativeLoans.Add(loan);
                await _context.SaveChangesAsync();

                Console.WriteLine("🟢 Loan successfully added.");
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 SERVER ERROR: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the loan.");
                var members = _context.CooperativeMembers.ToList(); // Fetch members again for dropdown
                ViewBag.Members = members;
                return View("AddLoanPage", loan); // Return the view with the model and error message
            }
        }

        [HttpGet]
        public IActionResult AddLoginDetails()
        {
            var members = _context.CooperativeMembers.ToList(); // Fetch members from DB

            if (members == null || !members.Any())
            {
                ViewBag.Members = new List<CooperativeMembers>(); // Ensure it's never null
            }
            else
            {
                ViewBag.Members = members;
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLoginDetails([FromForm] CooperativeUsers users)
        {
            Console.WriteLine("🟢 AddLoginDetails (POST) method triggered!");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("🔴 Model state is invalid.");
                var members = _context.CooperativeMembers.ToList(); // Fetch members again for dropdown
                ViewBag.Members = members;
                return View("AddLoginDetails", users); // Return the view with validation errors
            }

            try
            {
                Console.WriteLine($"🟢 Adding User for Member ID: {users.Member_Id}");

                _context.CooperativeUsers.Add(users);
                await _context.SaveChangesAsync();

                Console.WriteLine("🟢 User successfully added.");
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 SERVER ERROR: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the user.");
                var members = _context.CooperativeMembers.ToList(); // Fetch members again for dropdown
                ViewBag.Members = members;
                return View("AddLoginDetails", users); // Return the view with the model and error message
            }
        }
      
    }
}
