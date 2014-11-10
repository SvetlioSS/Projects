namespace BugTrackingSystem.Domain.Abstract
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericRepository<T>
        where T : class
    {
        IQueryable<T> All();

        T Find(Expression<Func<T, bool>> conditions);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
