using System.Data.Entity;

namespace ChattingServer.ServiceModel
{
    public class ClientContext : DbContext
    {
        public ClientContext()
            : base("ClientContext")
        {
        }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }
    }
}
