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
        // Show loan page with the member's loan data
        [HttpGet]
        public IActionResult LoanPage()
        {
            string userMemberId = HttpContext.Session.GetString("UserMemberId");
            if (string.IsNullOrEmpty(userMemberId))
            {
                return RedirectToAction("Index", "Login", new { area = "" }); // Redirect to login if session is empty
            }

            // Fetch loans based on the logged-in user's Member_Id
            var memberLoans = _context.CooperativeLoans
                                    .Where(loan => loan.Member_Id == int.Parse(userMemberId))
                                    .ToList();

            return View(memberLoans); // Pass the list directly to the view
        }


        // ✅ Show loan application form (GET)
        [HttpGet]
        public IActionResult LoanApplication()
        {
            // 🔹 Ensure user is logged in
            string userMemberId = HttpContext.Session.GetString("UserMemberId");
            if (string.IsNullOrEmpty(userMemberId))
            {
                return RedirectToAction("Index", "Login", new { area = "" }); // Redirect to login if session is empty
            }

            // 🔹 Pass a new instance of CooperativeLoans to the view
            return View(new CooperativeLoans());
        }

        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyLoan(CooperativeLoans loan)
        {
            Console.WriteLine("🚀 ApplyLoan method was triggered!");

            // ✅ Log the raw request body
            using (var reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8, true, 1024, true))
            {
                var rawBody = await reader.ReadToEndAsync();
                Console.WriteLine($"🔍 RAW REQUEST BODY: {rawBody}");
            }

            // ✅ Log received values before validation
            Console.WriteLine($"🔹 Loan Amount: {loan.Loan_Amount}");
            Console.WriteLine($"🔹 Purpose_Loan: {loan.Purpose_Loan}");
            Console.WriteLine($"🔹 Term: {loan.Term}");
            Console.WriteLine($"🔹 Status: {loan.Status}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ Model validation failed!");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"🔴 ERROR: {error.ErrorMessage}");
                }
                return View("LoanApplication", loan);
            }

            // 🔴 Prevent Division by Zero (Temporary Fix)
            if (loan.Term == 0 || loan.Loan_Amount == 0)
            {
                Console.WriteLine("❌ ERROR: Loan Amount or Term is 0, preventing calculation error.");
                ModelState.AddModelError("", "Loan Amount and Term must be greater than zero.");
                return View("LoanApplication", loan);
            }

            try
            {
                string userMemberId = HttpContext.Session.GetString("UserMemberId");
                if (string.IsNullOrEmpty(userMemberId))
                {
                    Console.WriteLine("❌ ERROR: User is not logged in!");
                    return RedirectToAction("Index", "Login", new { area = "" });
                }

                // ✅ Assign Member ID and Status
                loan.Member_Id = int.Parse(userMemberId);
                loan.Status = "Pending";
                loan.Release_Date = DateTime.Now;
                loan.First_Month = loan.Release_Date.AddMonths(1);
                loan.End_Month = loan.Release_Date.AddMonths(loan.Term);

                // ✅ Log Purpose_Loan before saving
                Console.WriteLine($"📝 Saving Loan with Purpose: {loan.Purpose_Loan}");

                // ✅ Calculate Loan Payment (Avoid Zero Division)
                decimal interestRate = (decimal)loan.Annual_Interest / 100 / 12;
                loan.Monthly_Payment = (loan.Loan_Amount * interestRate) /
                                       (1 - (decimal)Math.Pow((double)(1 + interestRate), -loan.Term));
                loan.Total_Payment = loan.Monthly_Payment * loan.Term;

                // ✅ Save to Database
                _context.CooperativeLoans.Add(loan);
                await _context.SaveChangesAsync();

                Console.WriteLine("✅ Loan application saved successfully!");
                TempData["SuccessMessage"] = "Loan application submitted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 ERROR: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while applying for the loan.");
                return View("LoanApplication", loan);
            }
        }








    }

}
