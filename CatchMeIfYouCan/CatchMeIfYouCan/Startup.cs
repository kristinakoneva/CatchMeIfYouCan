using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CatchMeIfYouCan.Startup))]
namespace CatchMeIfYouCan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
