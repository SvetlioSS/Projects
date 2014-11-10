namespace BugTrackingSystem.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using BugTrackingSystem.Domain.Abstract;
    using BugTrackingSystem.Domain.Models;
    using BugTrackingSystem.Domain.Infrastructure;
    using BugTrackingSystem.WebApi.Models;
    using BugTrackingSystem.WebApi.Infrastructure;

    [Authorize]
    public class BugsController : BaseController
    {
        private IUserIdProvider provider;

        public BugsController(IUowData database, IUserIdProvider provider)
            : base(database)
        {
            this.provider = provider;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var allBugViewModels = this.Database.Bugs.All().Select(bug =>
                new BugViewModel()
                {
                    AuthorId = bug.AuthorId,
                    AuthorName = bug.Author.UserName,
                    DateIssued = bug.DateIssued,
                    Description = bug.Description,
                    Id = bug.Id,
                    Name = bug.Name,
                    Priority = bug.Priority,
                    State = bug.State
                });

            return Ok(allBugViewModels);
        }

        public IHttpActionResult Get(int id)
        {
            Bug bugFound = this.Database.Bugs.Find(bug => bug.Id == id);
            if (bugFound == null)
            {
                return BadRequest("There is no Bug with the given id.");
            }

            BugViewModel bugViewModel = new BugViewModel()
            {
                AuthorId = bugFound.AuthorId,
                AuthorName = bugFound.Author.UserName,
                DateIssued = bugFound.DateIssued,
                Description = bugFound.Description,
                Id = bugFound.Id,
                Name = bugFound.Name,
                Priority = bugFound.Priority,
                State = bugFound.State
            };

            return Ok(bugViewModel);
        }

        [HttpPost]
        public IHttpActionResult Add(AddBugViewModel viewModel)
        {
            string userId = this.provider.GetUserId();

            Bug bug = new Bug()
            {
                Author = this.Database.Authors.Find(user => user.Id == userId),
                AuthorId = userId,
                DateIssued = DateTime.Now,
                Description = viewModel.Description,
                Name = viewModel.Name,
                Priority = (Priority)viewModel.Priority,
                State = BugState.NotFixed
            };
            this.Database.Bugs.Add(bug);
            this.Database.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(UpdateBugViewModel viewModel)
        {
            Bug bugFound = null;
            try
            {
                bugFound = this.Database.Bugs.Find(bug => bug.Id == viewModel.Id);
            }
            catch
            {
                return BadRequest("The id is missing or there is no bug with the given id.");
            }

            if (viewModel.Description != null)
            {
                bugFound.Description = viewModel.Description;
            }
            if (viewModel.Name != null)
            {
                bugFound.Name = viewModel.Name;
            }
            if (viewModel.Priority != null)
            {
                bugFound.Priority = (Priority)viewModel.Priority;
            }
            if (viewModel.State != null)
            {
                bugFound.State = (BugState)viewModel.State;
            }

            this.Database.Bugs.Update(bugFound);
            this.Database.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Bug bugFound = null;
            try
            {
                bugFound = this.Database.Bugs.Find(bug => bug.Id == id);
            }
            catch
            {
                return BadRequest("The id is missing or there is no bug with the given id.");
            }

            this.Database.Bugs.Delete(bugFound);
            this.Database.SaveChanges();

            return Ok();
        }
    }
}
