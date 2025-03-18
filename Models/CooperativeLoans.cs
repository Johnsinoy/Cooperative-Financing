using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CooperativeFinancing.Models
{
    public class CooperativeLoans
    {

        [Key]
        public int Loan_Id { get; set; } // ✅ Primary Key

        [Required(ErrorMessage = "Please select a member.")]
        public int Member_Id { get; set; } // ✅ Foreign Key

        [ForeignKey("Member_Id")]
        public CooperativeMembers? CooperativeMember { get; set; } // ✅ Make this optional

        [Required]
        public decimal Loan_Amount { get; set; }

        [Required]
        public string Purpose_Loan { get; set; }

        [Required]
        public float Annual_Interest { get; set; }

        [Required]
        public int Term { get; set; }

        [Required]
        public DateTime Release_Date { get; set; }

        [Required]
        public DateTime First_Month { get; set; }

        [Required]
        public DateTime End_Month { get; set; }

        [Required]
        public decimal Monthly_Payment { get; set; }

        [Required]
        public decimal Total_Payment { get; set; }

        [Required]
        public string Status { get; set; }


    }
}
