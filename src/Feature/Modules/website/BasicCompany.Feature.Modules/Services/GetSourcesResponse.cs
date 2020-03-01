using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Modules.Services
{
    public class GetSourcesResponse
    {
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public SourceItem[] items { get; set; }
    }

    public class SourceItem
    {
        public string __type { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public bool inReview { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }
}