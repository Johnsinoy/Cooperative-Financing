using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CooperativeFinancing.Models
{
    public class CooperativeLoans
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Loan_Id { get; set; }

        [Required]
        public int Member_Id { get; set; }
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

        // ✅ Navigation Property
        [ForeignKey("Member_Id")]
        public virtual CooperativeMembers CooperativeMember { get; set; }
    }
}
