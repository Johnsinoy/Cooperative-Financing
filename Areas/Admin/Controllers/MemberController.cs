using CooperativeFinancing.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

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

        // ✅ Generates and downloads the Member List PDF
        public IActionResult GenerateMemberListPdf()
        {
            var members = _context.CooperativeMembers
                .Select(m => new
                {
                    m.Member_Id,
                    FullName = m.FirstName + " " + m.LastName,
                    m.City,
                    m.Email,
                    m.Phone
                }).ToList();

            if (!members.Any())
            {
                return Content("No members found.");
            }

            using (var stream = new MemoryStream())
            {
                // ✅ Define the document properties
                Document document = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // ✅ Add title
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Paragraph title = new Paragraph("Member List Report", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Paragraph(" ")); // Add space

                // ✅ Create table with 5 columns
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1, 3, 2, 3, 3 }); // Column width ratios

                // ✅ Define header font
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                // ✅ Add table header
                table.AddCell(new PdfPCell(new Phrase("Member ID", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Full Name", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("City", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Email", headerFont)));
                table.AddCell(new PdfPCell(new Phrase("Phone", headerFont)));

                // ✅ Add member data rows
                Font rowFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                foreach (var member in members)
                {
                    table.AddCell(new PdfPCell(new Phrase(member.Member_Id.ToString(), rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(member.FullName, rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(member.City, rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(member.Email, rowFont)));
                    table.AddCell(new PdfPCell(new Phrase(member.Phone, rowFont)));
                }

                document.Add(table);
                document.Close(); // ✅ Close document before returning

                return File(stream.ToArray(), "application/pdf", "MemberList.pdf");
            }
        }
    }
}
