using System.Web;
using System.Web.Http;

namespace BookingSample.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SimpleInjectorWebApiInitializer.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}