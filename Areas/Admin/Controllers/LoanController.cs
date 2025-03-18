using CooperativeFinancing.Models;
using CooperativeFinancing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Admin.Controllers
{
    [Area("Admin")] // Ensure the controller belongs to the "Admin" area
    public class LoanController : Controller
    {
        private readonly CooperativeContext _context;

        public LoanController(CooperativeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LoanTable()
        {
            var loans = _context.MemberLoanDetails // Ensure MemberLoanDetails is mapped in DbContext
                .Select(l => new LoanViewModel
                {
                    Member_Id = l.Member_Id,
                    FirstName = l.FirstName,
                    LastName = l.LastName,
                    Loan_Amount = l.Loan_Amount,
                    Purpose_Loan = l.Purpose_Loan,
                    Annual_Interest = l.Annual_Interest,
                    Term = l.Term,
                    Release_Date = l.Release_Date,
                    First_Month = l.First_Month,
                    End_Month = l.End_Month,
                    Monthly_Payment = l.Monthly_Payment,
                    Total_Payment = l.Total_Payment,
                    Status = l.Status
                }).ToList();

            if (loans == null || !loans.Any()) // ✅ Prevents null reference issues
            {
                loans = new List<LoanViewModel>(); // Return an empty list if no data
            }

            return View(loans);
        }
    }
}
