using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugTrackingSystem.Web.Startup))]
namespace BugTrackingSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
