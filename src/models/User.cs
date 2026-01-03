using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace File_Access_Monitoring.Models
{
    public class User
    {
        [Key]
        public int UID { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string? PWD { get; set; }

        [NotMapped]
        [Compare("PWD", ErrorMessage ="Password Mismatch")]
        public string? REPWD { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression("^[6789][0-9]{9}$",  ErrorMessage ="Invalid Mobile Number")]
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? DeviceID { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public string UserType { get; set; } = "User";
    }
}
