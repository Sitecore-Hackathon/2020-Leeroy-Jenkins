
using BasicCompany.Feature.Modules.Controllers;
using BasicCompany.Feature.Modules.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace BasicCompany.Feature.Modules
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ModulesController>();
            serviceCollection.AddTransient<IMarketplaceCrawler, MarketplaceCrawler>();
            //System.Web.Http.GlobalConfiguration.Configuration.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }
    }
}