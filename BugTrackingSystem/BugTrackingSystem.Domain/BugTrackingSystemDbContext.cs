namespace BugTrackingSystem.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Microsoft.AspNet.Identity.EntityFramework;

    using BugTrackingSystem.Domain.Abstract;
    using BugTrackingSystem.Domain.Models;

    public class BugTrackingSystemDbContext : IdentityDbContext<Author>, IBugTrackingSystemDbContext
    {
        public BugTrackingSystemDbContext()
            : base("DConnection", throwIfV1Schema: false)
        {
        }

        public BugTrackingSystemDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {

        }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Bug> Bugs { get; set; }

        public override IDbSet<Author> Users
        {
            get
            {
                return base.Users;
            }
            set
            {
                base.Users = value;
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            if (typeof(TEntity) == typeof(Author))
            {
                IDbSet<TEntity> users = (IDbSet<TEntity>)base.Users;
                return users;
            }
            return base.Set<TEntity>();
        }

        public static BugTrackingSystemDbContext Create()
        {
            return new BugTrackingSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
