namespace CooperativeFinancing.Models.ViewModels
{
    public class LoginViewModel
    {
        public int User_Id { get; set; }
        public int Member_Id { get; set; }
        public string Member_Name { get; set; } // ✅ Full name from members table
        public string Username { get; set; }
        public string Password { get; set; } // ⚠️ Consider handling passwords securely
        public bool Is_Admin { get; set; }
        // ✅ Add ErrorMessage property to handle login errors
        public string errorMessage { get; set; }
    }
}
