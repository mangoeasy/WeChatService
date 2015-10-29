using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WeChatService.Web.Startup))]
namespace WeChatService.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
