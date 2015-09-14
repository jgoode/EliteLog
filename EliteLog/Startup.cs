using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EliteLog.Startup))]
namespace EliteLog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
