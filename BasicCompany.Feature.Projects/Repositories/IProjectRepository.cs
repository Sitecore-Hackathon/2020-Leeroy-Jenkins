using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicCompany.Feature.Projects.Models;
using Sitecore.Data.Items;

namespace BasicCompany.Feature.Projects.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<Item> GetProjectsByUser(Item parent);
        FeaturedProjectModel MapItemToModel(IEnumerable<Item> item);
    }

}
