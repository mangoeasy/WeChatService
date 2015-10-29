using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeChatService.Demo.Startup))]
namespace WeChatService.Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
