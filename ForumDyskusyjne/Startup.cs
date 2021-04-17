using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForumDyskusyjne.Startup))]
namespace ForumDyskusyjne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
