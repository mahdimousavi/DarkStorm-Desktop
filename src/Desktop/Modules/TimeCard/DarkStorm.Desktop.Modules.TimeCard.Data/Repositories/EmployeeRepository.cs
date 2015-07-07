using DarkStorm.Desktop.Infrastructure.Data.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Data.Repositories
{
    public partial class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public EmployeeRepository(TimeCardUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
