using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Modules.Services
{
    public class SearchModuleResponse
    {
        public IEnumerable<SitecoreModule> Modules { get; set; }
        public int Pages { get; set; }
    }
}