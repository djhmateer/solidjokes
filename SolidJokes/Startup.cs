using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolidJokes.Startup))]
namespace SolidJokes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
