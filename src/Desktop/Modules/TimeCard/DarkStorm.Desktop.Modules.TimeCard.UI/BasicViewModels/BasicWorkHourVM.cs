using DarkStorm.Desktop.Infrastructure.Domain.Core;
using System;
using System.Collections.Generic;

namespace DarkStorm.Desktop.Modules.TimeCard.UI.BasicViewModels
{
    public class BasicWorkHourVM : ITrackable
    {
        public BasicWorkHourVM()
        {
            ObjectState = ObjectState.Added;
        }
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<System.DateTime> DateWorked { get; set; }
        public Nullable<int> WorkCodeId { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public virtual BasicEmployeeVM Employee { get; set; }
        public virtual BasicWorkCodeVM WorkCode { get; set; }

        public string EmployeeFullName
        {
            get 
            {
               if(Employee!=null)
                    return Employee.FirstName+" "+Employee.LastName;
            else
                    return "";    
            }
        }

        public string WorkCodeDescription
        {
            get
            {
                if (WorkCode != null)
                    return WorkCode.Description;
                else
                    return "";
            }
        }
        public ObjectState ObjectState { get; set; }
    }
}
