using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.Data;

namespace BasicCompany.Feature.Projects.Repositories
{
    public class ProjectSearchQuery
    {
        [IndexField("_uniqueid")]
        public virtual ItemUri UniqueId { get; set; }
        [IndexField("Participants")]
        public virtual String Participants { get; set; }
        [IndexField("_path")]
        public virtual IEnumerable<ID> Paths { get; set; }
        [IndexField("_templates")]
        public virtual IEnumerable<ID> Templates { get; set; }
    }
}