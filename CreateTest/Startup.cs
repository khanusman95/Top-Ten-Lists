using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreateTest.Startup))]
namespace CreateTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
