using System.ComponentModel.DataAnnotations;

namespace File_Access_Monitoring.Models
{
    public class FileAccessLog
    {
        [Key]
        public int FALID { get; set; }
        public string Link { get; set; }
        public DateTime AccessOn { get; set; } = DateTime.Now;
        public int UID { get; set; } = 0;
        public string UserName { get; set; } = "UnAuthorized User";
        public string AccessType { get; set; } = "UnAuthorized";
    }
}
