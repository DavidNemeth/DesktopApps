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
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }
    }
}
