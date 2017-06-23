using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspFinalTest.Startup))]
namespace AspFinalTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
