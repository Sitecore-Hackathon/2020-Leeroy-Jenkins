using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Tasks;

namespace BasicCompany.Feature.Modules.Services
{
    public class MarketplaceCrawler : IMarketplaceCrawler
    {
        private readonly string MarketplaceUrl = "https://marketplace.sitecore.net";
        public MarketplaceCrawler()
        {

        }
        public async Task<SearchModuleResponse> Search(string term, int page, int count)
        {
            var searchRequest = new SearchRequest()
            {
                query = term,
                offset = page.ToString(),
                count = count.ToString(),
                category = "c1",
                requirement = string.Empty,
                support = string.Empty,
                familyIds = new string[] { }
            };

            using (var client = new HttpClient())
            {
                var url = $"{MarketplaceUrl}/Services/Modules.svc/Search";
                var payload = JsonConvert.SerializeObject(searchRequest);
                var data = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, data);
                var serializedResponse = await response.Content.ReadAsStringAsync();
                var searchModel = JsonConvert.DeserializeObject<SearchModel>(serializedResponse);
                return new SearchModuleResponse()
                {
                    Modules = searchModel.More.Items.Select(ToSitecoreModule),
                    Pages = searchModel.More.Total
                };
            }
        }

        private SitecoreModule ToSitecoreModule(Item item)
        {
            var id = GetId(item.Link);
            return new SitecoreModule()
            {
                Name = item.Modulename,
                Category = $"{item.Family} {item.CategoryName}",
                Id = id,
               // DownloadUrl = GetDownloadUrl(id).Result ?? string.Empty,
                Rate = item.Rate,
                Relevance = item.Relevance,
                Reviews = item.Reviews
            };
        }

        private async Task<string> GetDownloadUrl(ID id)
        {
            var downloadRequest = new GetSourcesRequest()
            {
                moduleId = id.ToString(),
                pageMode = false
            };

            using (var client = new HttpClient())
            {
                var url = $"{MarketplaceUrl}/Services/Modules.svc/GetSources";
                var payload = JsonConvert.SerializeObject(downloadRequest);
                var data = new StringContent(payload, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(payload, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                var serializedResponse = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<GetSourcesResponse>(serializedResponse);
                return model.items.FirstOrDefault(x => x.type == "Package")?.link;
            }
        }

        private ID GetId(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load($"{MarketplaceUrl}{url}");
            var hfModuleId = doc.GetElementbyId("hfModuleId");
            var id = hfModuleId.Attributes["value"].Value;
            return new ID(id);
        }
    }
}