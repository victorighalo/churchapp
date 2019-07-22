using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KingdomJoy.Startup))]
namespace KingdomJoy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
