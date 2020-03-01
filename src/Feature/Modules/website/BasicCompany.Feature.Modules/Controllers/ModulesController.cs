using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using BasicCompany.Feature.Modules.Models;
using BasicCompany.Feature.Modules.Services;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using Item = Sitecore.Data.Items.Item;

namespace BasicCompany.Feature.Modules.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ModulesController : Controller
    {
        protected readonly IMarketplaceCrawler MarketplaceCrawler;

        public ModulesController(IMarketplaceCrawler marketplaceCrawler)
        {
            MarketplaceCrawler = marketplaceCrawler;
        }

        [HttpPost]
        public ActionResult Vote(string Guid)
        {
            //var result = EditItem(Guid);
            var result = new VoteModel{Guid = Guid, success = true, votes = 10};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Search(string term, int page, int count)
        {
            var result = await MarketplaceCrawler.Search(term, page, count);
            //var result = new List<SitecoreModule>()
            //{
            //    new SitecoreModule()
            //    {
            //        Id = new ID(Guid.NewGuid()),
            //        Category = "Shared Source",
            //        Relevance = 3,
            //        Reviews = 2,
            //        Rate = 4,
            //        DownloadUrl = "www.google.com",
            //        Name = "Malicious Files Upload Blocker 1"
            //    },
            //    new SitecoreModule()
            //    {
            //        Id = new ID(Guid.NewGuid()),
            //        Category = "Shared Source",
            //        Relevance = 3,
            //        Reviews = 2,
            //        Rate = 4,
            //        DownloadUrl = "www.google.com",
            //        Name = "Malicious Files Upload Blocker 2"
            //    },
            //    new SitecoreModule()
            //    {
            //        Id = new ID(Guid.NewGuid()),
            //        Category = "Shared Source",
            //        Relevance = 3,
            //        Reviews = 2,
            //        Rate = 4,
            //        DownloadUrl = "www.google.com",
            //        Name = "Malicious Files Upload Blocker 3"
            //    },
            //    new SitecoreModule()
            //    {
            //        Id = new ID(Guid.NewGuid()),
            //        Category = "Shared Source",
            //        Relevance = 3,
            //        Reviews = 2,
            //        Rate = 4,
            //        DownloadUrl = "www.google.com",
            //        Name = "Malicious Files Upload Blocker 4"
            //    }
            //};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public VoteModel EditItem(String ItemGuid)
        {
            var item = Sitecore.Context.Database.GetItem(new ID(ItemGuid));
            item.Editing.BeginEdit();
            var countField = item.Fields["countFieldname"];
            var count = countField.Value;
            int.TryParse(count, out var result);
            result++;
            countField.Value = result.ToString();
            item.Editing.EndEdit();
            return new VoteModel{Guid = ItemGuid, success = true, votes = result};
        }
    }
}