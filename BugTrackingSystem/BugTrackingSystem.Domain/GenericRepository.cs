namespace BugTrackingSystem.Domain
{
    using System.Linq;
    using System.Data.Entity;
    using System;
    using System.Linq.Expressions;

    using BugTrackingSystem.Domain.Abstract;
    
    public class GenericRepository<T> : IGenericRepository<T> 
        where T : class
    {
        private IBugTrackingSystemDbContext dbContext;

        public GenericRepository()
            : this(new BugTrackingSystemDbContext())
        {
        }

        public GenericRepository(IBugTrackingSystemDbContext bugTrackingSystemDbContext)
        {
            this.dbContext = bugTrackingSystemDbContext; 
        }

        public IQueryable<T> All()
        {
            IQueryable<T> set = this.dbContext.Set<T>();
            return set;
        }

        public T Find(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions).FirstOrDefault();
        }

        public void Add(T entity)
        {
            ChangeState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            ChangeState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            ChangeState(entity, EntityState.Deleted);
        }

        private void ChangeState(T entity, EntityState state)
        {
            this.dbContext.Set<T>().Attach(entity);
            this.dbContext.Entry<T>(entity).State = state;
        }
    }
}
