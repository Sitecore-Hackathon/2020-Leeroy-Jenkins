using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Projects.Models
{
    public class ProjectModel
    {
        public string ModuleTitle { get; set; }
        public string ModuleDescription { get; set; }
        public string WebServerOs { get; set; }
        public string SitecoreProduct { get; set; }
        public string Owner { get; set; }
        public string Participants { get; set; }
        public decimal Price { get; set; }

    }
}