using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HockeyTeam.Startup))]
namespace HockeyTeam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
