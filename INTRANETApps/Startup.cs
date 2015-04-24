using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INTRANETApps.Startup))]
namespace INTRANETApps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
