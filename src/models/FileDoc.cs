using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace File_Access_Monitoring.Models
{
    public class FileDoc
    {
        [Key]
        public int FID { get; set; }
        [Required]
        public string? FileName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        
        public int DownloadCount { get; set; }
        public string? Extension { get; set; }

        public string? AuthorizedUserIDs { get; set; }
        [NotMapped]
        public List<int> UIDs{ get; set; }= new List<int>();

    }
}
