using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CavSoft.Startup))]
namespace CavSoft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
