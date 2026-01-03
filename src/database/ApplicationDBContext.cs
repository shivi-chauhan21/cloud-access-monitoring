using File_Access_Monitoring.Models;
using Microsoft.EntityFrameworkCore;

namespace File_Access_Monitoring.Database
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendance>? Attendances { get; set; }
        public DbSet<FileDoc>? File { get; set; }
        public DbSet<FileUserAccess>? FileUserAccesses { get; set; }
        public DbSet<FileAccessLog>? FileAccessLogs { get; set; }


    }
}
