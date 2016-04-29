using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(I_Shop.Startup))]
namespace I_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
