using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BasicCompany.Feature.Modules.Services
{
    public interface IMarketplaceCrawler
    {
        Task<SearchModuleResponse> Search(string term, int page, int count);
    }
}