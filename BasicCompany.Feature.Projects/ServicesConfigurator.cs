using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace BasicCompany.Feature.Projects
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<Controllers.ProjectsController>();
            serviceCollection.AddTransient<Repositories.IProjectRepository, Repositories.ProjectsRepository>();
        }
    }
}