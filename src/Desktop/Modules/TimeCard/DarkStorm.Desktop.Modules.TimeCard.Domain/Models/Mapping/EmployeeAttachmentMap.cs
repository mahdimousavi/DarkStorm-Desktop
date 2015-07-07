using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models.Mapping
{
    public class EmployeeAttachmentMap : EntityTypeConfiguration<EmployeeAttachment>
    {
        public EmployeeAttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("EmployeeAttachment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.Path).HasColumnName("Path");

            // Relationships
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.EmployeeAttachments)
                .HasForeignKey(d => d.EmployeeId);

        }
    }
}
