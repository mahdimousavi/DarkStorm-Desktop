using DarkStorm.Desktop.Infrastructure.Domain.Core;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels
{
    public partial class BasicWorkCodeVM :ITrackable
    {
        public BasicWorkCodeVM()
        {
            this.WorkHours = new List<BasicWorkHourVM>();
            ObjectState = ObjectState.Added;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Billable { get; set; }
        public virtual ICollection<BasicWorkHourVM> WorkHours { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}
