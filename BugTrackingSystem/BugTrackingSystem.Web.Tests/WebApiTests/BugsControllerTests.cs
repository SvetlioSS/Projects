namespace BugTrackingSystem.Web.Tests.WebApiTests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Net.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using BugTrackingSystem.Domain;
    using BugTrackingSystem.Domain.Abstract;
    using BugTrackingSystem.Domain.Models;
    using BugTrackingSystem.Domain.Infrastructure;
    using BugTrackingSystem.WebApi;
    using BugTrackingSystem.WebApi.Controllers;
    using BugTrackingSystem.WebApi.Models;
    using BugTrackingSystem.WebApi.Infrastructure;

    [TestClass]
    public class BugsControllerTests
    {
        [TestMethod]
        public void TestGetBugs_WhenNoGivenId_ShouldReturnAllBugs()
        {
            // Arrange
            List<Bug> repo = GetBugs();
            Mock<IUowData> mock = new Mock<IUowData>();
            mock.Setup(m => m.Bugs.All()).Returns(repo.AsQueryable<Bug>());
            Mock<IUserIdProvider> idProviderMock = new Mock<IUserIdProvider>();
            idProviderMock.Setup(m => m.GetUserId()).Returns("123idnewid123");
            BugsController controller = new BugsController(mock.Object, idProviderMock.Object);

            // Act
            var result = controller.Get() as OkNegotiatedContentResult<IQueryable<BugViewModel>>;

            // Assert
            Assert.AreEqual(repo.Count(), result.Content.Count());
        }

        [TestMethod]
        public void TestGetBug_WhenIdIsValidAndExists_ShouldReturnTheBugFound()
        {
            // Arrange
            List<Bug> repo = GetBugs();
            Mock<IUowData> dataMock = new Mock<IUowData>();
            dataMock.Setup(m => m.Bugs.Find(It.IsAny<Expression<Func<Bug, bool>>>())).Returns(repo[0]);
            Mock<IUserIdProvider> idProviderMock = new Mock<IUserIdProvider>();
            idProviderMock.Setup(m => m.GetUserId()).Returns("123idnewid123");
            BugsController controller = new BugsController(dataMock.Object, idProviderMock.Object);

            // Act
            var response = controller.Get(0);
            var result = response as OkNegotiatedContentResult<BugViewModel>;

            // Assert
            Assert.AreEqual(repo[0].Id, result.Content.Id);
            Assert.AreEqual(repo[0].Name, result.Content.Name);
            Assert.AreEqual(repo[0].Priority, result.Content.Priority);
            Assert.AreEqual(repo[0].State, result.Content.State);
            Assert.AreEqual(repo[0].Description, result.Content.Description);
            Assert.AreEqual(repo[0].AuthorId, result.Content.AuthorId);
        }

        [TestMethod]
        public void TestGetBug_WhenIdIsValidButDoesNotExist_ShouldReturnBadRequest()
        {
            // Arrange
            List<Bug> repo = GetBugs();
            Mock<IUowData> mock = new Mock<IUowData>();
            repo.Add(null);
            mock.Setup(m => m.Bugs.Find(It.IsAny<Expression<Func<Bug, bool>>>())).Returns(repo[10]);
            Mock<IUserIdProvider> idProviderMock = new Mock<IUserIdProvider>();
            idProviderMock.Setup(m => m.GetUserId()).Returns("123idnewid123");
            BugsController controller = new BugsController(mock.Object, idProviderMock.Object);

            // Act
            var response = controller.Get(0);
            var result = response as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAddBug_WhenBugIsValid_ShouldBeAddedToDb()
        {
            // Arrange
            List<Bug> repo = GetBugs();
            AddBugViewModel bug = new AddBugViewModel()
            {
                Description = "newly added bug",
                Name = "newly added",
                Priority = Priority.Low
            };

            Author author = new Author()
            {
                Id = "123idnewid123",
                UserName = "Gosho Test",
                Email = "someemail@some.email"
            };

            Mock<IUowData> mock = new Mock<IUowData>();
            mock.Setup(m => m.Bugs.Add(It.IsAny<Bug>())).Callback(() => repo.Add(new Bug()
            {
                Author = author,
                AuthorId = author.Id,
                Comments = null,
                DateIssued = DateTime.Now,
                Description = bug.Description,
                Id = 88,
                Name = bug.Name,
                Priority = (Priority)bug.Priority,
                State = BugState.NotFixed
            }));
            mock.Setup(m => m.Authors.Find(It.IsAny<Expression<Func<Author, bool>>>())).Returns(author);

            Mock<IUserIdProvider> idProviderMock = new Mock<IUserIdProvider>();
            idProviderMock.Setup(m => m.GetUserId()).Returns(author.Id);

            BugsController controller = new BugsController(mock.Object, idProviderMock.Object);

            // Act
            var response = controller.Add(bug);
            var result = response as OkResult;

            // Arrange
            Assert.IsNotNull(result);
            Assert.AreEqual(repo.ElementAt(repo.Count() - 1).Name, bug.Name);
            Assert.AreEqual(repo.ElementAt(repo.Count() - 1).Description, bug.Description);
        }

        private List<Bug> GetBugs()
        {
            Author author = new Author()
            {
                Id = "123",
                UserName = "Test"
            };

            List<Bug> repo = new List<Bug>();

            for (int i = 0; i < 10; i++)
            {
                Bug bug = new Bug()
                {
                    Author = author,
                    AuthorId = author.Id,
                    Comments = null,
                    DateIssued = DateTime.Now,
                    Description = "Test bug description " + i,
                    Name = "Test bug " + i,
                    Priority = Priority.Low,
                    State = BugState.NotFixed,
                    Id = i
                };

                repo.Add(bug);
            }

            return repo;
        }
    }
}
