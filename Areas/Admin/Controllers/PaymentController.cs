using CooperativeFinancing.Models;
using CooperativeFinancing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CooperativeFinancing.Areas.Admin.Controllers
{
    [Area("Admin")] // Ensure the controller belongs to the "Admin" area
    public class PaymentController : Controller
    {
        private readonly CooperativeContext _context;

        public PaymentController(CooperativeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult PaymentTable()
        {
            var payments = _context.MemberPaymentsView // Ensure this DbSet is mapped
             .Select(p => new PaymentViewModel
             {
                 Payment = new CooperativePayment
                 {
                     Payment_Id = p.Payment_Id,
                     Loan_Id = p.Loan_Id,
                     Member_Id = p.Member_Id,
                     Payment_Date = p.Payment_Date,
                     Payment_Amount = p.Payment_Amount
                 },
                 Member_Name = p.Member_Name // ✅ Fetch Member Name from the View
             }).ToList();

            return View(payments);
        }
    }
}
