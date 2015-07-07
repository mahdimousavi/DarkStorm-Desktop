using DarkStorm.Desktop.Infrastructure.Services.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Domain.IServices
{
    public partial interface IWorkHourService : IService<WorkHour>
    {
        IEnumerable<WorkHour> GetReport();
    }
}
