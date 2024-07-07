using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class StudentListEdit
    {
        public int StudentId { get; set; }

        [Required] //validation
        [Display(Name = "Name")] // display label name in value 
        public string StudentName { get; set; } = null!;

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Password")]
        public string StudentPassword { get; set; } = null!;

        [Required]
        [Display(Name = "Faculty")]
        public string Faculty { get; set; } = null!;
    }
}
