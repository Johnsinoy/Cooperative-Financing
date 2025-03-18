namespace CooperativeFinancing.Models.ViewModels
{
    public class PaymentViewModel
    {
        public CooperativePayment Payment { get; set; } = new CooperativePayment();
        public List<CooperativeMembers> Members { get; set; } = new List<CooperativeMembers>();
    }
}
