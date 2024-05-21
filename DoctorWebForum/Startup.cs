using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoctorWebForum.Startup))]
namespace DoctorWebForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
