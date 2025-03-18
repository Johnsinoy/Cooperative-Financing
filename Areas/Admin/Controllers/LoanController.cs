using CooperativeFinancing.Models;
using CooperativeFinancing.Models.ViewModels;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        // ✅ Generates and downloads the Loan List PDF
        public IActionResult GenerateLoanListPdf()
        {
            var loans = _context.CooperativeLoans
                .Include(l => l.CooperativeMember) // ✅ Join with Members table
                .Select(l => new
                {
                    l.Loan_Id,
                    MemberName = l.CooperativeMember.FirstName + " " + l.CooperativeMember.LastName, // ✅ Full Name
                    l.Loan_Amount,
                    l.Purpose_Loan,
                    l.Annual_Interest,
                    l.Status
                }).ToList();

            if (!loans.Any())
            {
                return Content("No loans found.");
            }

            using (var stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // ✅ Title
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Paragraph title = new Paragraph("Loan List Report", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                document.Add(new Paragraph(" ")); // Space

                // ✅ Create table with 6 columns
                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1, 3, 2, 3, 2, 2 }); // Adjust column ratios

                // ✅ Define header font
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                // ✅ Add table headers
                table.AddCell(new PdfPCell(new Phrase("Loan ID", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Member Name", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Loan Amount", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Purpose", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Interest (%)", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Status", headerFont)));

                // ✅ Add loan data rows
                Font rowFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                foreach (var loan in loans)
                {
                    table.AddCell(new PdfPCell(new Phrase(loan.Loan_Id.ToString(), rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(loan.MemberName, rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(loan.Loan_Amount.ToString("C"), rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(loan.Purpose_Loan, rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(loan.Annual_Interest.ToString("0.00"), rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(loan.Status, rowFont)));
                }

                document.Add(table);
                document.Close(); // ✅ Close document before returning

                return File(stream.ToArray(), "application/pdf", "LoanList.pdf");
            }
        }
    }
}
