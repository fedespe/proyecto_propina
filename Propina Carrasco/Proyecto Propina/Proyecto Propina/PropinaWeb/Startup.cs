using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PropinaWeb.Startup))]
namespace PropinaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
