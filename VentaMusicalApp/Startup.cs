using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VentaMusicalApp.Startup))]
namespace VentaMusicalApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
