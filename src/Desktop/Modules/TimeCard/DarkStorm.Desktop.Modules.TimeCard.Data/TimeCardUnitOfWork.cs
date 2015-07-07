using DarkStorm.Desktop.Infrastructure.Data.Core;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models;
using DarkStorm.Desktop.Modules.TimeCard.Domain.Models.Mapping;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DarkStorm.Desktop.Modules.TimeCard.Data
{
    public class TimeCardUnitOfWork : DbContext, IQueryableUnitOfWork
    {
        #region Constructor

        public TimeCardUnitOfWork()
            : base("name=TimeCard")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        #endregion Constructor

        #region IDbSet Members

        IDbSet<Employee> employees;
        public IDbSet<Employee> Employees
        {
            get
            {
                if (employees == null)
                    employees = base.Set<Employee>();

                return employees;
            }
        }

        IDbSet<EmployeeAttachment> employeeAttachments;
        public IDbSet<EmployeeAttachment> EmployeeAttachments
        {
            get
            {
                if (employeeAttachments == null)
                    employeeAttachments = base.Set<EmployeeAttachment>();

                return employeeAttachments;
            }
        }

        IDbSet<Filter> filters;
        public IDbSet<Filter> Filters
        {
            get
            {
                if (filters == null)
                    filters = base.Set<Filter>();

                return filters;
            }
        }

        IDbSet<WorkCode> workCodes;
        public IDbSet<WorkCode> WorkCodes
        {
            get
            {
                if (workCodes == null)
                    workCodes = base.Set<WorkCode>();

                return workCodes;
            }
        }

        IDbSet<WorkHour> workHours;
        public IDbSet<WorkHour> WorkHours
        {
            get
            {
                if (workHours == null)
                    workHours = base.Set<WorkHour>();

                return workHours;
            }
        }
        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<T> CreateSet<T>()
            where T : class
        {
            return base.Set<T>();
        }

        public void Attach<T>(T item)
            where T : class
        {
            //attach and set as unchanged
            base.Entry<T>(item).State = EntityState.Unchanged;
        }

        public void SetModified<T>(T item)
            where T : class
        {
            //this operation also attach item in object state manager
            base.Entry<T>(item).State = EntityState.Modified;
        }
        public void ApplyCurrentValues<T>(T original, T current)
            where T : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<T>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new EmployeeAttachmentMap());
            modelBuilder.Configurations.Add(new FilterMap());
            modelBuilder.Configurations.Add(new WorkCodeMap());
            modelBuilder.Configurations.Add(new WorkHourMap());
        }
        #endregion

    }
}
