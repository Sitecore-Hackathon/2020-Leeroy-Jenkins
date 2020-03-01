using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Modules.Services
{
    public class SearchModel
    {
        public object Exact { get; set; }
        public More More { get; set; }
    }

    public class More
    {
        public Item[] Items { get; set; }
        public int ItemsOnPage { get; set; }
        public int Total { get; set; }
    }

    public class Item
    {
        public string CategoryId { get; set; }
        public string CategoryKey { get; set; }
        public string CategoryName { get; set; }
        public string CategoryShortId { get; set; }
        public string Description { get; set; }
        public string Family { get; set; }
        public string FamilyId { get; set; }
        public string FamilyShortId { get; set; }
        public int Like { get; set; }
        public string Link { get; set; }
        public string Modulename { get; set; }
        public float Rate { get; set; }
        public int Relevance { get; set; }
        public int Reviews { get; set; }
    }
}
