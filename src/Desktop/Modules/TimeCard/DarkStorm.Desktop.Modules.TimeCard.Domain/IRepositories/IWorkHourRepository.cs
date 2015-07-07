using DarkStorm.Desktop.Infrastructure.Domain.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories
{ 
    public partial interface IWorkHourRepository : IRepository<WorkHour>
    {
        IEnumerable<WorkHour> GetReport();
    }
}
