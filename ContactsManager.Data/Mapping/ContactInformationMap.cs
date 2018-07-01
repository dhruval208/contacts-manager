#region Namespaces

using System.Data.Entity.ModelConfiguration;

#endregion

namespace ContactsManager.Data.Models.Mapping
{
    /// <summary>
    /// ContactInformationMap
    /// </summary>
    public class ContactInformationMap : EntityTypeConfiguration<ContactInformation>
    {
        /// <summary>
        /// ContactInformationMap
        /// </summary>
        public ContactInformationMap()
        {
            // Primary Key - UserId
            this.HasKey(t => t.UserId);

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
