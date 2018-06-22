using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VetSoft.Presentation.Startup))]
namespace VetSoft.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
