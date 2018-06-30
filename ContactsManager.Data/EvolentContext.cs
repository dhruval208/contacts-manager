using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ContactsManager.Data.Models.Mapping;

namespace ContactsManager.Data.Models
{
    public partial class EvolentContext : DbContext
    {
        static EvolentContext()
        {
            Database.SetInitializer<EvolentContext>(null);
        }

        public EvolentContext()
            : base("Name=EvolentContext")
        {
        }

        public DbSet<ContactInformation> ContactInformations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactInformationMap());
        }
    }
}
