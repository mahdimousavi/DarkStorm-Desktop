using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models.Mapping
{
    public class WorkCodeMap : EntityTypeConfiguration<WorkCode>
    {
        public WorkCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WorkCode");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Billable).HasColumnName("Billable");
        }
    }
}
