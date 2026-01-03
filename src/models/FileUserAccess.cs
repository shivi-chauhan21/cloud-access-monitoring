using System.ComponentModel.DataAnnotations;

namespace File_Access_Monitoring.Models
{
    public class FileUserAccess
    {
        [Key]
        public int FUAID { get; set; }

        [Required]
        public int UID { get; set; }
    }
}
