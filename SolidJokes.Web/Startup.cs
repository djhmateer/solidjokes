using Microsoft.Owin;
using Owin;
using SolidJokes.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace SolidJokes.Web {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
