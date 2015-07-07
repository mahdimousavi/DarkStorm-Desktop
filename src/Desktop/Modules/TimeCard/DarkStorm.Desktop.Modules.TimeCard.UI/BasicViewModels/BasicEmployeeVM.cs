using DarkStorm.Desktop.Infrastructure.Domain.Core;
using System;
using System.Collections.Generic;

namespace DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels
{
    public class BasicEmployeeVM : ITrackable
    {
        public BasicEmployeeVM()
        {
            this.EmployeeAttachments = new List<BasicEmployeeAttachmentVM>();
            this.WorkHours = new List<BasicWorkHourVM>();
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
        public virtual ICollection<BasicEmployeeAttachmentVM> EmployeeAttachments { get; set; }
        public virtual ICollection<BasicWorkHourVM> WorkHours { get; set; }
        public ObjectState ObjectState { get; set; }

        public override string ToString()
        {
            return FirstName+" "+LastName;
        }
    }
}
