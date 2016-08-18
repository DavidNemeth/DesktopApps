using ChatModel.Users;
using System.Data.Entity;

namespace ChatModel
{
    public class ProfilesContext : DbContext
    {
        public ProfilesContext()
            : base("Profiles")
        {
        }
        public DbSet<ProfileModel> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }
    }
}
