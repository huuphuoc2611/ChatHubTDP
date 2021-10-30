using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatHubTDP.Startup))]
namespace ChatHubTDP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
