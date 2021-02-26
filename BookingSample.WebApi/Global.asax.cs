using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BookingSample.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SimpleInjectorWebApiInitializer.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
