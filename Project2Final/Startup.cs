using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project2Final.Startup))]
namespace Project2Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
