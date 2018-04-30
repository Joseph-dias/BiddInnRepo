using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bidd_Inn.Startup))]
namespace Bidd_Inn
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
