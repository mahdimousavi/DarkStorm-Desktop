using DarkStorm.Desktop.Infrastructure.Services;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IRepositories;
using DarkStorm.Desktop.Modules.TimeCard.Domain.IServices;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Services
{
    public partial class EmployeeAttachmentService : Service<EmployeeAttachment>, IEmployeeAttachmentService
    {
        #region Constructor

        /// <summary>
        /// EmployeeAttachmentService
        /// </summary>
        public EmployeeAttachmentService(IEmployeeAttachmentRepository repository)
            : base(repository)
        {
        }

        #endregion
    }
}
