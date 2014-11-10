namespace BugTrackingSystem.WebApi.Infrastructure
{
    using System.Security.Principal;

    using Microsoft.AspNet.Identity;

    public class AspNetUserIdProvider : IUserIdProvider
    {
        private IPrincipal principal;

        public AspNetUserIdProvider(IPrincipal principal)
        {
            this.principal = principal;
        }

        public string GetUserId()
        {
            return this.principal.Identity.GetUserId();
        }
    }
}