namespace BugTrackingSystem.Web.Tests.WebApiTests
{

    using System;
    using System.Transactions;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BugTrackingSystem.Domain;
    using BugTrackingSystem.Domain.Models;
    using BugTrackingSystem.Domain.Infrastructure;
    using BugTrackingSystem.Domain.Abstract;

    [TestClass]
    public class RepositoryTests
    {
        private const string ConnectionString = @"Data Source=060-PC\SQLEXPRESS;Initial Catalog=BugTrackingSystem;Integrated Security=True";

        static TransactionScope tran;
        private BugTrackingSystemDbContext db;

        public RepositoryTests()
        {
            this.db = new BugTrackingSystemDbContext(ConnectionString);
        }

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            tran.Dispose();
        }
        
        [TestMethod]
        public void AddBug_WhenBugIsValid_ShouldAddBugToDb()
        {
            // Arrange
            Author author = this.db.Users.FirstOrDefault();
            Bug bug = GetValidBug(author);

            // Act
            this.db.Bugs.Add(bug);
            this.db.SaveChanges();
            Bug bugFound = this.db.Bugs.Find(bug.Id);

            // Assert
            Assert.IsNotNull(this.db);
            Assert.IsNotNull(author);
            Assert.IsNotNull(bug);
            Assert.IsNotNull(bugFound);
        }

        [TestMethod]
        public void RemoveBug_WhenBugExists_ShouldRemoveBugFromDb()
        {
            // Arrange
            Author author = this.db.Users.FirstOrDefault();
            Assert.IsNotNull(author);

            Bug bug = GetValidBug(author);
            Assert.IsNotNull(bug);

            // Act
            this.db.Bugs.Add(bug);
            this.db.SaveChanges();
            var bugFound = this.db.Bugs.Find(bug.Id);
            Assert.IsNotNull(bugFound);

            this.db.Bugs.Remove(bug);
            this.db.SaveChanges();
            var bugAfterRemoval = this.db.Bugs.Find(bug.Id);

            // Assert
            Assert.IsNull(bugAfterRemoval);
        }

        private Bug GetValidBug(Author author)
        {   
            Bug bug = new Bug()
            {
                Author = author,
                AuthorId = author.Id,
                Comments = null,
                DateIssued = DateTime.Now,
                Description = "Test bug's description.",
                Name = "Test bug from Unit Tests.",
                Priority = Priority.Low,
                State = BugState.NotFixed
            };

            return bug;
        }
    }
}
