namespace BugTrackingSystem.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using BugTrackingSystem.Domain;
    using BugTrackingSystem.Domain.Abstract;
    using BugTrackingSystem.Domain.Models;
    using BugTrackingSystem.Domain.Infrastructure;
    using BugTrackingSystem.Web.Models;

    [Authorize]
    public class HomeController : BaseController
    {
        private const int PageSize = 10;

        public HomeController(IUowData database)
            : base(database)
        {
        }

        public ActionResult Index(int page = 1, bool showDetails = false)
        {
            PagingViewModel pageViewModel = GetPagingViewModel(page, showDetails);

            if (Request.IsAjaxRequest())
            {
                if (showDetails)
                {
                    return PartialView("_FullBugDetailsPartial", pageViewModel);
                }
                else
                {
                    return PartialView("_HalfBugDetailsPartial", pageViewModel);
                }
            }

            return View(pageViewModel);
        }

        [HttpGet]
        public ActionResult AddBugGet(AddBugViewModel addBugViewModel)
        {
            return View(addBugViewModel);
        }

        [HttpPost]
        public ActionResult AddBug(AddBugViewModel addBugViewModel, string description,
            string name, int priority)
        {
            this.Database.Bugs.Add(
                new Bug()
                {
                    AuthorId = this.User.Identity.GetUserId(), 
                    DateIssued = DateTime.Now,
                    Description = description,
                    Name = name,
                    Priority = (Priority)priority,
                    State = BugState.NotFixed
                });
            this.Database.SaveChanges();

            PagingViewModel pageViewModel = GetPagingViewModel(GetTotalNumberOfPages(), addBugViewModel.ShowDetails);

            return View("Index", pageViewModel);
        }

        [HttpGet]
        public ActionResult UpdateBugGet(PagingViewModel pagingViewModel, int id)
        {
            pagingViewModel.ViewModels = new[] 
                { 
                    GetBugViewModelById(id)
                };

            return View("UpdateBug", pagingViewModel);
        }

        [HttpPost]
        public ActionResult UpdateBug(PagingViewModel pagingViewModel, BugViewModel bugViewModel)
        {
            var bugFound = this.Database.Bugs.Find(bug => bug.Id == bugViewModel.Id);
            bugFound.Name = bugViewModel.Name;
            bugFound.Priority = bugViewModel.Priority;
            bugFound.State = bugViewModel.State;
            bugFound.Description = bugViewModel.Description;

            this.Database.Bugs.Update(bugFound);
            this.Database.SaveChanges();

            PagingViewModel pageViewModelToSend = GetPagingViewModel(pagingViewModel.Page, pagingViewModel.ShowDetails);

            return View("Index", pageViewModelToSend);
        }

        public ActionResult RemoveBug(PagingViewModel pagingViewModel, int id)
        {
            var bugFound = this.Database.Bugs.Find(bug => bug.Id == id);

            int count = bugFound.Comments.Count();
            for (int i = 0; i < count; i++)
            {
                this.Database.Comments.Delete(bugFound.Comments.ElementAt(0));
            }
            this.Database.SaveChanges();

            this.Database.Bugs.Delete(bugFound);
            this.Database.SaveChanges();

            PagingViewModel pageViewModelToSend = GetPagingViewModel(GetTotalNumberOfPages(), pagingViewModel.ShowDetails);

            return View("Index", pageViewModelToSend);
        }

        [HttpGet]
        public ActionResult CommentGet(PagingViewModel pagingViewModel, int id)
        {
            pagingViewModel.ViewModels = new[] { GetBugViewModelById(id) };

            CommentViewModel commentViewModel = new CommentViewModel()
            {
                PagingViewModel = pagingViewModel,
                Comments = GetCommentsByBugId(id)
            };

            return View("CommentGet", commentViewModel);
        }

        [HttpPost]
        public ActionResult Comment(int id, string content)
        {
            string userId = this.User.Identity.GetUserId();
            Comment comment = new Comment()
            {
                AuthorId = userId,
                BugId = id,
                Author = this.Database.Authors.Find(author => author.Id == userId),
                Bug = this.Database.Bugs.Find(bug => bug.Id == id),
                Content = content,
                DateCreated = DateTime.Now
            };
            this.Database.Comments.Add(comment);
            this.Database.SaveChanges();

            return PartialView("_CommentPartial", comment);
        }

        private IEnumerable<Comment> GetCommentsByBugId(int id)
        {
            var bugFound = this.Database.Bugs.Find(bug => bug.Id == id);

            return bugFound.Comments.OrderByDescending(comment => comment.DateCreated);
        }

        private BugViewModel GetBugViewModelById(int id)
        {
            var bugFound = this.Database.Bugs.Find(bug => bug.Id == id);
            var bugViewModel = new BugViewModel()
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

            return bugViewModel;
        }

        private int GetTotalNumberOfPages()
        {
            var viewModelsCount = GetViewModels().Count<BugViewModel>();
            int totalPages = Convert.ToInt32(Math.Ceiling((double)viewModelsCount / PageSize));
            return totalPages;
        }

        private PagingViewModel GetPagingViewModel(int page, bool showDetails)
        {
            var viewModels = GetViewModels().Skip((page - 1) * PageSize).Take(PageSize);

            PagingViewModel pageViewModel = new PagingViewModel()
            {
                ShowDetails = showDetails,
                BugStartingIndex = (page - 1) * PageSize + 1,
                Page = page,
                TotalPages = GetTotalNumberOfPages(),
                ViewModels = viewModels
            };

            return pageViewModel;
        }

        private IEnumerable<BugViewModel> GetViewModels()
        {
            var viewModels = this.Database.Bugs.All().Select(bug => new BugViewModel()
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

            return viewModels.OrderBy(bug => bug.DateIssued);
        }
    }
}