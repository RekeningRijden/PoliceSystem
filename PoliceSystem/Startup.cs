using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoliceSystem.Startup))]
namespace PoliceSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
