using DarkStorm.Desktop.Infrastructure.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models
{
    public partial class WorkCode : ITrackable
    {
        public WorkCode()
        {
            this.WorkHours = new List<WorkHour>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Billable { get; set; }
        public virtual ICollection<WorkHour> WorkHours { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
