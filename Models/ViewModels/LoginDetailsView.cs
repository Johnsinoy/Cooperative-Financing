namespace CooperativeFinancing.Models.ViewModels
{
    public class LoginDetailsView
    {
        public CooperativeUsers Users { get; set; } = new CooperativeUsers();
        public List<CooperativeMembers> Members { get; set; } = new List<CooperativeMembers>();
        public int User_Id { get; set; }
        public int Member_Id { get; set; }
        public string Member_Name { get; set; } // ✅ Full name from members table
        public string Username { get; set; }
        public string Password { get; set; } // ⚠️ Consider handling passwords securely
        public bool Is_Admin { get; set; }
    }
}
