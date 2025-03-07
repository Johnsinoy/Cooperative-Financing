namespace CooperativeFinancing.Models.ViewModels
{
    public class LoginDetailsView
    {
        public CooperativeUsers Users { get; set; } = new CooperativeUsers();
        public List<CooperativeMembers> Members { get; set; } = new List<CooperativeMembers>();
    }
}
