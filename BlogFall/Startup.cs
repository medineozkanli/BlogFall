using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogFall.Startup))]
namespace BlogFall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
