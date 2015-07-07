using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.Company)
                .HasMaxLength(50);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(50);

            this.Property(t => t.JobTitle)
                .HasMaxLength(50);

            this.Property(t => t.BusinessPhone)
                .HasMaxLength(50);

            this.Property(t => t.HomePhone)
                .HasMaxLength(50);

            this.Property(t => t.MobilePhone)
                .HasMaxLength(50);

            this.Property(t => t.FaxNumber)
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(50);

            this.Property(t => t.City)
                .HasMaxLength(50);

            this.Property(t => t.State)
                .HasMaxLength(50);

            this.Property(t => t.ZIPCode)
                .HasMaxLength(50);

            this.Property(t => t.Country)
                .HasMaxLength(50);

            this.Property(t => t.WebPage)
                .HasMaxLength(50);

            this.Property(t => t.Notes)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Employee");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Company).HasColumnName("Company");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.BusinessPhone).HasColumnName("BusinessPhone");
            this.Property(t => t.HomePhone).HasColumnName("HomePhone");
            this.Property(t => t.MobilePhone).HasColumnName("MobilePhone");
            this.Property(t => t.FaxNumber).HasColumnName("FaxNumber");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.ZIPCode).HasColumnName("ZIPCode");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.WebPage).HasColumnName("WebPage");
            this.Property(t => t.Notes).HasColumnName("Notes");
        }
    }
}
