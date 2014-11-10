namespace BugTrackingSystem.Domain
{
    using System;
    using System.Collections.Generic;

    using Ninject.MVC;

    using BugTrackingSystem.Domain.Abstract;
    using BugTrackingSystem.Domain.Models;
    using System.Data.Entity;

    public class UowData : IUowData
    {
        private readonly BugTrackingSystemDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData()
            : this(new BugTrackingSystemDbContext())
        {
        }

        public UowData(BugTrackingSystemDbContext bugTrackingSystemDbContext)
        {
            this.context = bugTrackingSystemDbContext;
        }

        public IGenericRepository<Bug> Bugs
        {
            get
            {
                return this.GetRepository<Bug>();
            }
        }

        public IGenericRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IGenericRepository<Author> Authors
        {
            get
            {
                return this.GetRepository<Author>();
            }
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
