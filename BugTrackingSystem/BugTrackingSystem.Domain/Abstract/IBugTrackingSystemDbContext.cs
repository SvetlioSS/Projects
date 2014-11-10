namespace BugTrackingSystem.Domain.Abstract
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    using Microsoft.AspNet.Identity.EntityFramework;

    using BugTrackingSystem.Domain.Models;

    public interface IBugTrackingSystemDbContext
    {
        IDbSet<Bug> Bugs { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
