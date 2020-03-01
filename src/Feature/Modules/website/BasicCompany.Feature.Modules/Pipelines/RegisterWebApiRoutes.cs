using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace BasicCompany.Feature.Modules.Pipelines
{
    public class RegisterWebApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Modules", "api/{controller}/{action}", new { controller = "Modules", action = "Search" });
            RouteTable.Routes.MapRoute("ModulesVotes", "api/{controller}/{action}", new { controller = "Modules", action = "Vote" });
        }
    }
}