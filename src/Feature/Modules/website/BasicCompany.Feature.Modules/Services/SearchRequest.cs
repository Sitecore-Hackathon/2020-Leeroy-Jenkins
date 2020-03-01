using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Modules.Services
{
    public class SearchRequest
    {
        public string query { get; set; }
        public string offset { get; set; }
        public string count { get; set; }
        public string category { get; set; }
        public string requirement { get; set; }
        public string[] familyIds { get; set; }
        public string support { get; set; }
    }
}