using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CRMContracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,bool trackChanges);
        void Create(T entity);
        void CreateEntities(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }

}
