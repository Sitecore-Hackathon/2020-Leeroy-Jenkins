using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Modules.Services
{
    public class GetSourcesRequest
    {
        public string moduleId { get; set; }
        public bool pageMode { get; set; }
    }
}