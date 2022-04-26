using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSVWebApplication1.Startup))]
namespace CSVWebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
