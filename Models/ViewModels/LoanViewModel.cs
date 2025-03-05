namespace CooperativeFinancing.Models.ViewModels
{
    public class LoanViewModel
    {
        public CooperativeLoans Loan { get; set; } = new CooperativeLoans();
        public List<CooperativeMembers> Members { get; set; } = new List<CooperativeMembers>();
    }
}
