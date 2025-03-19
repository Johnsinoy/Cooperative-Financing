using CooperativeFinancing.Models;
using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Users.Controllers
{
    [Area("Users")]
    public class LoanController : Controller
    {
        private readonly CooperativeContext _context;

        // ✅ Inject the database context
        public LoanController(CooperativeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // ✅ Show loan application form
        [HttpGet]
        public IActionResult LoanPage()
        {
            return View();
        }

        // ✅ Handle loan application submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyLoan(CooperativeLoans loan)
        {
            if (!ModelState.IsValid)
            {
                return View(loan);
            }

            try
            {
                // 🔹 Assign the logged-in user's Member ID
                string userMemberId = HttpContext.Session.GetString("UserMemberId");
                if (string.IsNullOrEmpty(userMemberId))
                {
                    return RedirectToAction("Index", "Login", new { area = "" }); // Redirect if not logged in
                }

                loan.Member_Id = int.Parse(userMemberId);
                loan.Status = "Pending"; // 🔹 Default status for new loans

                _context.CooperativeLoans.Add(loan);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Loan application submitted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while applying for the loan.");
                Console.WriteLine($"🔴 ERROR: {ex.Message}");
                return View(loan);
            }
        }
    }
}
