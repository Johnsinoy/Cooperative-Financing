using CooperativeFinancing.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CooperativeFinancing.Areas.Admin.Controllers
{
    [Area("Admin")] // ✅ Ensure this controller belongs to the "Admin" area
    public class LoanController : Controller
    {
        private readonly CooperativeContext _context;

        public LoanController(CooperativeContext context)
        {
            _context = context;
        }

        // ✅ Fetch All Loans for the Admin Loan Table
        [HttpGet]
        public async Task<IActionResult> LoanTable()
        {
            var loans = await _context.CooperativeLoans
                .Include(l => l.CooperativeMember) // ✅ Ensure it joins with the member details
                .ToListAsync();

            return View(loans);
        }

        // ✅ Generate Loan List PDF for Admin Reports
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
                Paragraph title = new Paragraph("Loan List Report", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);
                document.Add(new Paragraph(" ")); // Space

                // ✅ Create Table
                PdfPTable table = new PdfPTable(6)
                {
                    WidthPercentage = 100
                };
                table.SetWidths(new float[] { 1, 3, 2, 3, 2, 2 }); // Column width ratios

                // ✅ Define Fonts
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                Font rowFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // ✅ Add Table Headers
                string[] headers = { "Loan ID", "Member Name", "Loan Amount", "Purpose", "Interest (%)", "Status" };
                foreach (var header in headers)
                {
                    table.AddCell(new PdfPCell(new Phrase(header, headerFont)));
                }

                // ✅ Add Loan Data Rows
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
                document.Close();

                return File(stream.ToArray(), "application/pdf", "LoanList.pdf");
            }
        }

        // ✅ Update Loan Details
        [HttpPost]
        public async Task<IActionResult> UpdateLoan([FromForm] CooperativeLoans updatedLoan)
        {
            try
            {
                Console.WriteLine("🚀 UpdateLoan method triggered!");
                Console.WriteLine($"🔍 Incoming Data: Loan_Id={updatedLoan.Loan_Id}, Amount={updatedLoan.Loan_Amount}");

                // ✅ Ensure Loan_Id is valid
                if (updatedLoan.Loan_Id <= 0)
                {
                    Console.WriteLine("❌ ERROR: Invalid Loan_Id received.");
                    return BadRequest("Invalid loan ID.");
                }

                // ✅ Check if Loan Exists
                var existingLoan = await _context.CooperativeLoans.FindAsync(updatedLoan.Loan_Id);
                if (existingLoan == null)
                {
                    Console.WriteLine("❌ ERROR: Loan not found!");
                    return NotFound("Loan not found.");
                }

                // ✅ Check for Missing Fields Before Updating
                if (updatedLoan.Loan_Amount <= 0)
                {
                    return BadRequest("Loan amount must be greater than zero.");
                }
                if (string.IsNullOrEmpty(updatedLoan.Purpose_Loan))
                {
                    return BadRequest("Purpose of the loan is required.");
                }
                if (updatedLoan.Term <= 0)
                {
                    return BadRequest("Invalid loan term.");
                }
                if (string.IsNullOrEmpty(updatedLoan.Status))
                {
                    return BadRequest("Loan status is required.");
                }

                // ✅ Update Loan Data
                existingLoan.Loan_Amount = updatedLoan.Loan_Amount;
                existingLoan.Purpose_Loan = updatedLoan.Purpose_Loan;
                existingLoan.Term = updatedLoan.Term;
                existingLoan.Status = updatedLoan.Status;

                // ✅ Save changes
                await _context.SaveChangesAsync();

                Console.WriteLine("✅ Loan updated successfully!");
                return Ok("Loan updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the loan.");
            }
        }

        // ✅ Delete Loan
        [HttpPost]
        public async Task<IActionResult> DeleteLoan([FromForm] int Loan_Id)
        {
            try
            {
                Console.WriteLine("🚀 DeleteLoan method triggered!");
                Console.WriteLine($"🔍 Deleting Loan ID: {Loan_Id}");

                // ✅ Ensure Loan_Id is valid
                if (Loan_Id <= 0)
                {
                    Console.WriteLine("❌ ERROR: Invalid Loan_Id received.");
                    return BadRequest("Invalid loan ID.");
                }

                // ✅ Find Loan in Database
                var loan = await _context.CooperativeLoans.FindAsync(Loan_Id);
                if (loan == null)
                {
                    Console.WriteLine("❌ ERROR: Loan not found!");
                    return NotFound("Loan not found.");
                }

                // ✅ Remove Loan and Save
                _context.CooperativeLoans.Remove(loan);
                await _context.SaveChangesAsync();

                Console.WriteLine("✅ Loan deleted successfully!");
                return Ok("Loan deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the loan.");
            }
        }
    }
}
