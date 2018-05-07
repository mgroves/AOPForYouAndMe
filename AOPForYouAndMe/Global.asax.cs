using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;

namespace AOPForYouAndMe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> {new Uri("http://localhost:8091")}
            }, new PasswordAuthenticator("matt", "password"));
        }
    }
}
