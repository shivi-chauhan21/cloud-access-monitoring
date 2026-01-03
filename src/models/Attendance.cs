using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace File_Access_Monitoring.Models
{
    public class Attendance
    {
        [Key]
        public int AID { get; set; }
        [Required]
        public int UID { get; set; }
        
        public DateTime? CheckIn { get; set; } = null;
        public DateTime? CheckOut { get; set; } = null;

        public int? Minutes { get; set; } = 0;
        public int? Hours { get; set; } = 0;

        [NotMapped]
        public TimeSpan? Duration { get; set; }

        public void Update()
        {
            if (this.CheckOut == null) return;

            this.Minutes = (this.CheckOut - this.CheckIn).GetValueOrDefault().Minutes;
            this.Hours = (this.CheckOut - this.CheckIn).GetValueOrDefault().Hours;
            this.Duration =new TimeSpan ( this.Hours.Value, this.Minutes.Value % 60,0);

        }
    }
}
