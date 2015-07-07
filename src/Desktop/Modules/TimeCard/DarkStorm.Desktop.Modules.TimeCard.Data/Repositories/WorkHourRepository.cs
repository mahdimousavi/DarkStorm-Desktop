using DarkStorm.Desktop.Infrastructure.Data.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DarkStorm.Desktop.Modules.TimeCard.Data.Repositories
{
    public partial class WorkHourRepository : Repository<WorkHour>, IWorkHourRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public WorkHourRepository(TimeCardUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        public IEnumerable<WorkHour> GetReport()
        {
            return GetSet().AsNoTracking<WorkHour>().Include(a=>a.WorkCode).Include(a=>a.Employee);
        }
        #endregion
    }
}
