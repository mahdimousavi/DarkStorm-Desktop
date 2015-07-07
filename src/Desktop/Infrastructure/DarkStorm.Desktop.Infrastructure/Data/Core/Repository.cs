using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq.Dynamic;
using DarkStorm.Desktop.Infrastructure.Domain.Core;
using DarkStorm.Desktop.Infrastructure.Logging;
using DarkStorm.Desktop.Infrastructure.Domain.Core.Specification;

namespace DarkStorm.Desktop.Infrastructure.Data.Core
{
    public class Repository<T> : IRepository<T>
    where T : class
    {
        #region Members

        IQueryableUnitOfWork _UnitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {

            if (item != (T)null)
                GetSet().Add(item); // add new item in this set
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo("Message.info_CannotAddNullEntity", typeof(T).ToString());

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(T item)
        {
            if (item != (T)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo("Message.info_CannotRemoveNullEntity", typeof(T).ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void TrackItem(T item)
        {
            if (item != (T)null)
                _UnitOfWork.Attach<T>(item);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo("Message.info_CannotRemoveNullEntity", typeof(T).ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Modify(T item)
        {
            if (item != (T)null)
            {
                //_UnitOfWork.Attach(item);
                _UnitOfWork.SetModified(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo("Message.info_CannotRemoveNullEntity", typeof(T).ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(object id)
        {
            if (id != null)
                return GetSet().Find(id);
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return GetSet().AsNoTracking<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> AllMatching(ISpecification<T> specification)
        {
            return GetSet().Where(specification.SatisfiedBy()).AsNoTracking<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<T, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).AsNoTracking<T>();
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetFiltered(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return GetSet().Where(filter).AsNoTracking<T>();
        }

        public virtual IEnumerable<T> GetFiltered(string filter)
        {
            return GetSet().Where(filter).AsNoTracking<T>();
        }
        public virtual IEnumerable<T> GetFiltered(string filter, string sort)
        {
            return GetSet().Where(filter).OrderBy(sort).AsNoTracking<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="persisted"></param>
        /// <param name="current"></param>
        public virtual void Merge(T persisted, T current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return GetSet().Where(where).AsNoTracking<T>().ToList();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        protected IDbSet<T> GetSet()
        {
            return (IDbSet<T>)_UnitOfWork.CreateSet<T>();
        }
        #endregion

    }
}
