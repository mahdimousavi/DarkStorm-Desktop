using DarkStorm.Desktop.Infrastructure.Services;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Services
{
    public partial class WorkHourService : Service<WorkHour>, IWorkHourService
    {
        private IWorkHourRepository repository;
        #region Constructor

        /// <summary>
        /// WorkHourService
        /// </summary>
        public WorkHourService(IWorkHourRepository repository)
            : base(repository)
        {
            this.repository = repository;
        }

        #endregion
        public IEnumerable<WorkHour> GetReport()
        {
            return repository.GetReport();
        }
    }
}
