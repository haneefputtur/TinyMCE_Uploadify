using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Haneef_Uploadify.Startup))]
namespace Haneef_Uploadify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
