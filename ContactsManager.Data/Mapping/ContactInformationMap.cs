using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContactsManager.Data.Models.Mapping
{
    public class ContactInformationMap : EntityTypeConfiguration<ContactInformation>
    {
        public ContactInformationMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("ContactInformation");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
