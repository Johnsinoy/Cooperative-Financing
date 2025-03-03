using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CooperativeFinancing.Models
{
    public class CooperativePayment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Payment_Id { get; set; }

        [Required]
        public int Loan_Id { get; set; }

        [Required]
        public int Member_Id { get; set; }  // ✅ Ensure Foreign Key Property Exists

        [Column(TypeName = "date")]
        public DateTime Payment_Date { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Payment_Amount { get; set; }
    }
}
