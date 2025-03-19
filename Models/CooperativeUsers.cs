using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CooperativeFinancing.Models
{
    public class CooperativeUsers
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        [Required]
        [ForeignKey("CooperativeMember")] // ✅ Foreign Key linking to CooperativeMembers
        public int Member_Id { get; set; }
        public CooperativeMembers CooperativeMember { get; set; } // ✅ Navigation Property (Singular Name)


        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Is_Admin { get; set; }
    }
}
