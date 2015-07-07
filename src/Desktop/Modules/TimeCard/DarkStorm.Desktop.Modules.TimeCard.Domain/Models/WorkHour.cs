using DarkStorm.Desktop.Infrastructure.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models
{
    public partial class WorkHour : ITrackable
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<System.DateTime> DateWorked { get; set; }
        public Nullable<int> WorkCodeId { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual WorkCode WorkCode { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
