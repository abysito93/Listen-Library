using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lis_Lib.Startup))]
namespace Lis_Lib
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
