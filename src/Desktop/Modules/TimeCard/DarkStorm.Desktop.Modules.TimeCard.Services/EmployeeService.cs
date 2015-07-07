using DarkStorm.Desktop.Infrastructure.Services;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Services
{
    public partial class EmployeeService : Service<Employee>, IEmployeeService
    {
        #region Constructor

        /// <summary>
        /// EmployeeService
        /// </summary>
        public EmployeeService(IEmployeeRepository repository)
            : base(repository)
        {
        }

        #endregion
    }
}
