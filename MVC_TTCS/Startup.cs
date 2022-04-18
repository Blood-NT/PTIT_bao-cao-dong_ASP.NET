using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_TTCS.Startup))]
namespace MVC_TTCS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
