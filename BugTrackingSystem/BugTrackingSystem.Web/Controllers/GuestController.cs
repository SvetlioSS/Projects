namespace BugTrackingSystem.Web.Controllers
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    using BugTrackingSystem.Domain.Abstract;
    using BugTrackingSystem.Web.Models;

    public class GuestController : BaseController
    {
        public GuestController(IUowData database)
            : base(database)
        {            
        }

        public ActionResult Index()
        {
            return View(GetGuestViewModels());
        }

        private IEnumerable<GuestViewModel> GetGuestViewModels()
        {
            var guestViewModels = this.Database.Bugs.All()
                .OrderBy(bug => bug.DateIssued)
                .Take(10)
                .Select(bug => new GuestViewModel
                    {
                        BugName = bug.Name,
                        Description = bug.Description
                    });

            return guestViewModels;
        }
    }
}