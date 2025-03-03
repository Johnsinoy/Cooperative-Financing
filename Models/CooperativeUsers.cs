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
        public int Member_Id { get; set; }  // ✅ Ensure correct Foreign Key

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Is_Admin { get; set; }
    }
}
