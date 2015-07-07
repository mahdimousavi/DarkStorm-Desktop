using DarkStorm.Desktop.Infrastructure.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models
{
    public partial class Employee : ITrackable
    {
        public Employee()
        {
            this.EmployeeAttachments = new List<EmployeeAttachment>();
            this.WorkHours = new List<WorkHour>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public string BusinessPhone { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
        public string Country { get; set; }
        public string WebPage { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<EmployeeAttachment> EmployeeAttachments { get; set; }
        public virtual ICollection<WorkHour> WorkHours { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
