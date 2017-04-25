using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sph.WebSite.Startup))]
namespace Sph.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
