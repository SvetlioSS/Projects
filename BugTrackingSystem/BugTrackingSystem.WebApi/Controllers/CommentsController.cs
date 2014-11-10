namespace BugTrackingSystem.WebApi.Controllers
{
    using System;
    using System.Web.Http;
    using System.Collections;
    using System.Linq;
    using System.Data.Entity.Validation;

    using Microsoft.AspNet.Identity;

    using BugTrackingSystem.Domain.Abstract;
    using BugTrackingSystem.WebApi.Models;
    using BugTrackingSystem.Domain.Models;
    using System.Collections.Generic;

    public class CommentsController : BaseController
    {
        public CommentsController(IUowData database)
            : base(database)
        {
        }
                
        // TODO: Fix problem with update, implement remove.        

        [HttpGet]
        public IHttpActionResult Get()
        {
            var commentViewModels = this.Database.Comments.All().Select(comment =>
                new CommentViewModel()
                {
                    AuthorId = comment.AuthorId,
                    BugId = comment.BugId,
                    Content = comment.Content,
                    DateCreated = comment.DateCreated,
                    Id = comment.Id
                });

            return Ok(commentViewModels);
        }
                
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Comment commentFound = this.Database.Comments.Find(comment => comment.Id == id);
            if (commentFound == null)
            {
                return BadRequest("There is no Comment with the given id.");
            }

            CommentViewModel commentViewModel = new CommentViewModel()
            {
                AuthorId = commentFound.AuthorId,
                BugId = commentFound.BugId,
                Content = commentFound.Content,
                DateCreated = commentFound.DateCreated,
                Id = commentFound.Id
            };

            return Ok(commentViewModel);
        }
                
        [HttpPost]
        public IHttpActionResult Add(AddCommentViewModel viewModel)
        {
            Bug bugFound = this.Database.Bugs.Find(bug => bug.Id == viewModel.BugId);
            if (bugFound == null)
            {
                return BadRequest("There is no bug with the given id.");
            }

            string userId = this.User.Identity.GetUserId(); 
            Comment comment = new Comment()
            {
                AuthorId = userId,
                Author = this.Database.Authors.Find(user => user.Id == userId),
                BugId = viewModel.BugId,
                Bug = bugFound,
                Content = viewModel.Content,
                DateCreated = DateTime.Now,
            };

            this.Database.Comments.Add(comment);
            this.Database.SaveChanges();

            return Ok();
        }
                
        [HttpPut]
        public IHttpActionResult Update(UpdateCommentViewModel viewModel)
        {
            Comment commentFound = this.Database.Comments.Find(comment => comment.Id == viewModel.Id);
            if (commentFound == null)
            {
                return BadRequest("There is no Comment with the given id.");
            }

            // Explicit call to .Bug and .Author to take the values, 
            // otherwise the values won't be taken due to lazy loading.
            Bug bug = commentFound.Bug;
            Author author = commentFound.Author;
            commentFound.Bug = bug;
            commentFound.Author = author;

            if (viewModel.Content != null)
            {
                commentFound.Content = viewModel.Content;
            }

            this.Database.Comments.Update(commentFound);
            this.Database.SaveChanges();

            return Ok();
        }
                
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            Comment commentFound = this.Database.Comments.Find(comment => comment.Id == id);
            if (commentFound == null)
            {
                return BadRequest("There is no Comment with the given id.");
            }

            this.Database.Comments.Delete(commentFound);
            this.Database.SaveChanges();

            return Ok();
        }
    }
}