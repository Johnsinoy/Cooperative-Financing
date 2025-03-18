using CooperativeFinancing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CooperativeFinancing.Areas.Admin.Controllers
{
    [Area("Admin")] // Ensure the controller belongs to the "Admin" area
    public class MemberController : Controller
    {
        private readonly CooperativeContext _context;

        public MemberController(CooperativeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult MemberProfile(int id)
        {
            var member = _context.CooperativeMembers
                .Include(m => m.CooperativeLoans) // Include the member's loans
                .FirstOrDefault(m => m.Member_Id == id);

            if (member == null)
            {
                return NotFound(); // Return 404 if the member is not found
            }

            return View(member);
        }

    }
}
