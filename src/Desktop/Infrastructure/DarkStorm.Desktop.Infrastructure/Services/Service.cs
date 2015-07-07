using DarkStorm.Desktop.Infrastructure.Domain.Core;
using DarkStorm.Desktop.Infrastructure.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkStorm.Desktop.Infrastructure.Services
{
    public class Service<T> : IService<T>
        where T : class
    {
        protected IRepository<T> Repository;

        public Service(IRepository<T> repository)
        {
            Repository = repository;
        }
        public void Add(T item)
        {
            Repository.Add(item);
        }

        public void Remove(object id)
        {
            Repository.Remove(Repository.Get(id));
        }
        public void Modify(T item)
        {
            Repository.Modify(item);
        }

        public void TrackItem(T item)
        {
            Repository.TrackItem(item);
        }

        public void Merge(T persisted, T current)
        {
            Repository.Merge(persisted,current);
        }

        public T Get(object id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public IEnumerable<T> AllMatching(Domain.Core.Specification.ISpecification<T> specification)
        { 
            return Repository.AllMatching(specification).ToList();
        }

        public IEnumerable<T> GetPaged<Property>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<T, Property>> orderByExpression, bool ascending)
        {
            return Repository.GetPaged<Property>(pageIndex, pageCount, orderByExpression, ascending);
        }

        public IEnumerable<T> GetFiltered(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return Repository.GetFiltered(filter);
        }
        public virtual bool Save()
        {
            Repository.UnitOfWork.Commit();
            return true;
        }
        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}
