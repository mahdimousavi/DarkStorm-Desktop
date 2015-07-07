using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models.Mapping
{
    public class FilterMap : EntityTypeConfiguration<Filter>
    {
        public FilterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ObjectName)
                .HasMaxLength(50);

            this.Property(t => t.FilterName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Filter");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ObjectName).HasColumnName("ObjectName");
            this.Property(t => t.FilterName).HasColumnName("FilterName");
            this.Property(t => t.FilterString).HasColumnName("FilterString");
            this.Property(t => t.SortString).HasColumnName("SortString");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
