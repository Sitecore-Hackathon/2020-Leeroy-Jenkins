using Sitecore.Pipelines.HttpRequest;

namespace BasicCompany.Project.BasicCompany.Pipelines
{
    public class AccessControlHeaders : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            args.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            args.HttpContext.Response.AppendHeader("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS");
            args.HttpContext.Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
        }
    }
}