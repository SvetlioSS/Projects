namespace BugTrackingSystem.Domain.Abstract
{
    using System;

    using BugTrackingSystem.Domain.Models;
    
    public interface IUowData : IDisposable
    {
        IGenericRepository<Author> Authors { get; }

        IGenericRepository<Bug> Bugs { get; }

        IGenericRepository<Comment> Comments { get; }

        int SaveChanges();
    }
}
