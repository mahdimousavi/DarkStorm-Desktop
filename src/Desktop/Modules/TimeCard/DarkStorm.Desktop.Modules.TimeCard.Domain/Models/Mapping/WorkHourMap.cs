using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models.Mapping
{
    public class WorkHourMap : EntityTypeConfiguration<WorkHour>
    {
        public WorkHourMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("WorkHour");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.DateWorked).HasColumnName("DateWorked");
            this.Property(t => t.WorkCodeId).HasColumnName("WorkCodeId");
            this.Property(t => t.Hours).HasColumnName("Hours");

            // Relationships
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.WorkHours)
                .HasForeignKey(d => d.EmployeeId);
            this.HasOptional(t => t.WorkCode)
                .WithMany(t => t.WorkHours)
                .HasForeignKey(d => d.WorkCodeId);

        }
    }
}
