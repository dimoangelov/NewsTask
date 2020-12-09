using NewsTask.Services.Contracts;
using NewsTask.Services.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsTask.Services
{
    public class NewsService : INewsService
    {
        private const string ApiKey = "a0c9f4e4938c4d709f3dfab68e0471f4";
        private readonly IHttpClientFactory clientFactory;

        public NewsService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task GetTopHeadlines()
        {
            var client = this.clientFactory.CreateClient();

            var response = await client.GetAsync($"http://newsapi.org/v2/top-headlines?country=us&apiKey={ApiKey}");

            var responseAsString = await response.Content.ReadAsStringAsync();

            var jObj = JObject.Parse(responseAsString);

            IList<JToken> result = jObj["articles"].Children().ToList();

            IList<Article> listArticles = new List<Article>();

            foreach (JToken article in result)
            {
                Article art = article.ToObject<Article>();
                listArticles.Add(art);
            }
        }
    }
}