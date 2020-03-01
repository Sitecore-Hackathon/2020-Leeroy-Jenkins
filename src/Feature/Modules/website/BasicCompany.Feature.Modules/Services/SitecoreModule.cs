using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace BasicCompany.Feature.Modules.Services
{
    public class SitecoreModule
    {
        public ID Id { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }
        public int Relevance { get; set; }
        public int Reviews { get; set; }
        public string Category { get; set; }
        public string DownloadUrl { get; set; }
    }
}