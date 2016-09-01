using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DchatEntities
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
