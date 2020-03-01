using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace BasicCompany.Feature.Accounts
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<Controllers.AccountController>();
            serviceCollection.AddTransient<Repositories.IAccountRepository, Repositories.AccountRepository>();
            serviceCollection.AddTransient<Pipelines.IPipelineService, Pipelines.PipelineService>();
        }
    }
}