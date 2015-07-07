using DarkStorm.Desktop.Infrastructure.Data.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Data.Repositories
{
    public partial class WorkCodeRepository : Repository<WorkCode>, IWorkCodeRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public WorkCodeRepository(TimeCardUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
