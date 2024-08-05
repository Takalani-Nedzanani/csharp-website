using Microsoft.Owin;
using Owin;
using firebase1.App_Start;




[assembly: OwinStartupAttribute(typeof(firebase1.App_Start.StartUp))]
namespace firebase1.App_Start
{
    public partial class StartUp
    {
public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}