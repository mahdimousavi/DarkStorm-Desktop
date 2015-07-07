using DarkStorm.Desktop.Infrastructure.Services;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Services
{
    public partial class WorkCodeService : Service<WorkCode>, IWorkCodeService
    {
        #region Constructor

        /// <summary>
        /// WorkCodeService
        /// </summary>
        public WorkCodeService(IWorkCodeRepository repository)
            : base(repository)
        {
        }

        #endregion
    }
}
