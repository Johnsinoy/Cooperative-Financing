using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CooperativeFinancing.Models
{
    public class CooperativeMembers
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Member_Id { get; set; }

        [Required]
        [StringLength(45)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(45)]
        public string LastName { get; set; }

        [StringLength(45)]
        public string Street { get; set; }

        [StringLength(45)]
        public string City { get; set; }

        [StringLength(45)]
        public string Province { get; set; }

        [Required]
        public string Phone { get; set; }  // Using string to support various phone formats

        [Required]
        public string Email { get; set; }  // LongText in MySQL maps to string

        [Column(TypeName = "date")]
        public DateTime JoinDate { get; set; }
        public int Contribution { get; set; }
    }
}
