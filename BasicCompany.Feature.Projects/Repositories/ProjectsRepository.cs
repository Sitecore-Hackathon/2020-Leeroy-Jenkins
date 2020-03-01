using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using BasicCompany.Feature.Projects.Models;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Security.Authentication;

namespace BasicCompany.Feature.Projects.Repositories
{
    public class ProjectsRepository : IProjectRepository
    {
        public IEnumerable<Item> GetProjectsByUser(Item parent)
        {
            var user = AuthenticationManager.GetActiveUser();
            using (var context = GetSearchContext(parent))
            {
                var results = context.GetQueryable<ProjectSearchQuery>()
                    .Where(product => product.Paths.Contains(parent.ID) && product.Templates.Contains(Templates.Project.Id))
                    .Where(x => x.Participants.Contains(user.Name))
                    .Select(x => new {
                        Uri = x.UniqueId,
                        Database = Factory.GetDatabase(x.UniqueId.DatabaseName)
                    }).ToList();


                return results.Select(x => ItemManager.GetItem(x.Uri.ItemID, x.Uri.Language, x.Uri.Version, x.Database));
            }
        }

        public FeaturedProjectModel MapItemToModel(IEnumerable<Item> items)
        {
            var projectList = new FeaturedProjectModel
            {
                FeaturedProjects = new List<ProjectModel>()
            };
            foreach (var item in items)
            {
                projectList.FeaturedProjects.Add(new ProjectModel() { ModuleTitle = item["ModuleTitle"], ModuleDescription = item["ModuleDescription"], SitecoreProduct = item["SitecoreProduct"], WebServerOs = item["WebServerOS"] });
            }
            return projectList;
        }

        protected virtual IProviderSearchContext GetSearchContext(Item item)
        {
            return ContentSearchManager.GetIndex((SitecoreIndexableItem)item).CreateSearchContext();
        }
    }
}