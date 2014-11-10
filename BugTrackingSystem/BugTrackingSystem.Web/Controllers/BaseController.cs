namespace BugTrackingSystem.Web.Controllers
{
    using System.Web.Mvc;

    using BugTrackingSystem.Domain.Abstract;

    public abstract class BaseController : Controller
    {
        private IUowData database;

        protected BaseController(IUowData database)
        {
            this.database = database;
        }

        protected IUowData Database
        {
            get
            {
                return this.database;
            }
            private set
            {
                this.database = value;
            }
        }
    }
}