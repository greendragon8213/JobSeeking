using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPMainProject.Startup))]
namespace ASPMainProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
