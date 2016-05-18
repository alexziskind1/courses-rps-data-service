using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RPSDataService.Startup))]

namespace RPSDataService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}