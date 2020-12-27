using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebShopping.Startup))]
namespace WebShopping
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
