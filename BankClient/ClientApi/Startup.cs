using ClientApi;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("ClientStartup", typeof(ClientApi.Startup))]
//[assembly: OwinStartup(typeof(Startup))]

namespace ClientApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
