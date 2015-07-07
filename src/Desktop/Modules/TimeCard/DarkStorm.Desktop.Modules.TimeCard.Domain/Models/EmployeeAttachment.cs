using DarkStorm.Desktop.Infrastructure.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.Models
{
    public partial class EmployeeAttachment : ITrackable
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string Path { get; set; }
        public virtual Employee Employee { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
