using System.Data.Entity;

namespace DchatDb
{
    public class DchatContext : DbContext       
    {
        public DchatContext()
            :base("Dchat")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
    }
}
