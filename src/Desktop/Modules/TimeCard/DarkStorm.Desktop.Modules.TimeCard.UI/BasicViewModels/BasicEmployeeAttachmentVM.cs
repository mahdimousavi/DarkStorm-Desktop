using DarkStorm.Desktop.Infrastructure.Domain.Core;
using System;
using System.Collections.Generic;

namespace DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels
{
    public partial class BasicEmployeeAttachmentVM : ITrackable
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string Path { get; set; }
        public virtual BasicEmployeeVM Employee { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}
