using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheNewFacebook.Startup))]
namespace TheNewFacebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
           app.MapSignalR();

        }
    }
}
