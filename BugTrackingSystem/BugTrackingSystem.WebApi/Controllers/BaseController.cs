namespace BugTrackingSystem.WebApi.Controllers
{
    using System.Web.Http;

    using BugTrackingSystem.Domain.Abstract;

    public class BaseController : ApiController
    {
        private IUowData database;

        protected BaseController(IUowData database)
        {
            this.Database = database;
        }

        protected IUowData Database
        {
            get
            {
                return this.database;
            }
            set
            {
                this.database = value;
            }
        }
    }
}