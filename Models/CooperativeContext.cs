using Microsoft.EntityFrameworkCore;
using CooperativeFinancing.Models.ViewModels; // Make sure to include the ViewModel namespace

namespace CooperativeFinancing.Models
{
    public class CooperativeContext : DbContext
    {
        public CooperativeContext(DbContextOptions<CooperativeContext> options)
            : base(options)
        {
        }
        public DbSet<CooperativeLoans> CooperativeLoans { get; set; }
        public DbSet<CooperativePayment> CooperativePayment { get; set; }
        public DbSet<CooperativeUsers> CooperativeUsers { get; set; }
        public DbSet<CooperativeMembers> CooperativeMembers { get; set; }

        public DbSet<MemberLoanDetails> MemberLoanDetails { get; set; }
        public DbSet<MemberPaymentsView> MemberPaymentsView { get; set; }
        public DbSet<LoginDetailsView> UserMembersView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map the MySQL View (Ensure the view name matches exactly in MySQL)
            modelBuilder.Entity<MemberLoanDetails>().ToView("MemberLoanDetails").HasKey(l => l.Member_Id);

            // ✅ Ensure the MySQL View is mapped correctly
            modelBuilder.Entity<MemberPaymentsView>().ToView("MemberPaymentsView").HasKey(p => p.Payment_Id);

            // ✅ Ensure the MySQL View is mapped correctly
            modelBuilder.Entity<LoginDetailsView>().ToView("UserMembersView").HasKey(u => u.User_Id);


            // Seeding CooperativeMembers
            modelBuilder.Entity<CooperativeMembers>().HasData(
                new CooperativeMembers { Member_Id = 1, FirstName = "John", LastName = "Doe", Street = "123 Elm St", City = "Springfield", Province = "Illinois", Phone = "123-456-7890", Email = "john.doe@example.com", JoinDate = new DateTime(2023, 5, 15), Contribution = 600 },
                new CooperativeMembers { Member_Id = 2, FirstName = "Jane", LastName = "Smith", Street = "456 Oak Ave", City = "Los Angeles", Province = "California", Phone = "987-654-3210", Email = "jane.smith@example.com", JoinDate = new DateTime(2023, 7, 22), Contribution = 500 }
            );

            // Seeding Loans
            modelBuilder.Entity<CooperativeLoans>().HasData(
                new CooperativeLoans
                {
                    Loan_Id = 1,
                    Member_Id = 1,
                    Loan_Amount = 5000.00m,
                    Purpose_Loan = "Business Expansion",
                    Annual_Interest = 5.5f,
                    Term = 24,
                    Release_Date = new DateTime(2023, 6, 1),
                    First_Month = new DateTime(2023, 7, 1),
                    End_Month = new DateTime(2025, 6, 1),
                    Monthly_Payment = 230.00m,
                    Total_Payment = 5520.00m,
                    Status = "Active"
                },
                new CooperativeLoans
                {
                    Loan_Id = 2,
                    Member_Id = 2,
                    Loan_Amount = 7000.00m,
                    Purpose_Loan = "Home Renovation",
                    Annual_Interest = 6.2f,
                    Term = 36,
                    Release_Date = new DateTime(2023, 8, 10),
                    First_Month = new DateTime(2023, 9, 10),
                    End_Month = new DateTime(2026, 8, 10),
                    Monthly_Payment = 215.00m,
                    Total_Payment = 7740.00m,
                    Status = "Active"
                }
            );

            // Seeding Payments
            modelBuilder.Entity<CooperativePayment>().HasData(
                new CooperativePayment { Payment_Id = 1, Loan_Id = 1, Member_Id = 1, Payment_Date = new DateTime(2023, 6, 15), Payment_Amount = 500.00m },
                new CooperativePayment { Payment_Id = 2, Loan_Id = 1, Member_Id = 1, Payment_Date = new DateTime(2023, 7, 15), Payment_Amount = 500.00m },
                new CooperativePayment { Payment_Id = 3, Loan_Id = 2, Member_Id = 2, Payment_Date = new DateTime(2023, 9, 5), Payment_Amount = 700.00m }
            );

        }

    }

}
