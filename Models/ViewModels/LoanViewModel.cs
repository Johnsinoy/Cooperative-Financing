namespace CooperativeFinancing.Models.ViewModels
{
    public class LoanViewModel
    {
        public CooperativeLoans Loan { get; set; } = new CooperativeLoans();
        public List<CooperativeMembers> Members { get; set; } = new List<CooperativeMembers>();

        public int Member_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Loan_Amount { get; set; }
        public string Purpose_Loan { get; set; }
        public float Annual_Interest { get; set; }
        public int Term { get; set; }
        public DateTime Release_Date { get; set; }
        public DateTime First_Month { get; set; }
        public DateTime End_Month { get; set; }
        public decimal Monthly_Payment { get; set; }
        public decimal Total_Payment { get; set; }
        public string Status { get; set; }
    }
}
