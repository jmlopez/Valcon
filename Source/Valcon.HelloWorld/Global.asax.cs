using System;
using System.Web;
using System.Web.Routing;
using Valcon.HelloWorld.Configuration;

namespace Valcon.HelloWorld
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs args)
        {
            var routes = RouteTable.Routes;
            FubuStructureMapBootstrapper.Bootstrap(routes);
        }
    }
}
