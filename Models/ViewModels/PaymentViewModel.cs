namespace CooperativeFinancing.Models.ViewModels
{
    public class PaymentViewModel
    {
        public CooperativePayment Payment { get; set; } = new CooperativePayment();
        public List<CooperativeMembers> Members { get; set; } = new List<CooperativeMembers>();
        public int Payment_Id { get; set; } // Primary Key from payments table
        public int Loan_Id { get; set; }
        public int Member_Id { get; set; }

        public string Member_Name { get; set; } // ✅ FirstName + LastName from members table

        public DateTime Payment_Date { get; set; }
        public decimal Payment_Amount { get; set; }
    }
}
