using NewsTask.Data.Enums;
using NewsTask.Data.Models;
using NewsTask.Services.Contracts;
using Newtonsoft.Json.Linq;
using System;
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

        public async Task<IList<Article>> GetTopHeadlines(int pageSize, Category category, Country country)
        {
            var client = this.clientFactory.CreateClient();


            var response = await client.GetAsync($"http://newsapi.org/v2/top-headlines?" +
                $"country={country}" +
                $"&pagesize={pageSize}" +
                $"&category={category}" +
                $"&apiKey={ApiKey}");

            var responseAsString = await response.Content.ReadAsStringAsync();

            var jObj = JObject.Parse(responseAsString);

            IList<JToken> resultList = jObj["articles"].Children().ToList();

            IList<Article> listArticles = new List<Article>();

            foreach (JToken item in resultList)
            {
                Article article = item.ToObject<Article>();
                listArticles.Add(article);
            }

            return listArticles;
        }

        public async Task<IList<Article>> SearchArchive(string searchString, int pageSize, SortOrder sort, DateTime from, DateTime to)
        {
            var client = this.clientFactory.CreateClient();


            var response = await client.GetAsync($"http://newsapi.org/v2/everything?" +
                $"q={searchString}" +
                $"&pagesize={pageSize}" +
                $"&sortBy={sort}" +
                $"&from={from}" +
                $"&to={to}" +
                $"&apiKey={ApiKey}");


            var responseAsString = await response.Content.ReadAsStringAsync();

            var jObj = JObject.Parse(responseAsString);

            IList<JToken> resultList = jObj["articles"].Children().ToList();

            IList<Article> listArticles = new List<Article>();

            foreach (JToken item in resultList)
            {
                Article article = item.ToObject<Article>();
                listArticles.Add(article);
            }

            return listArticles;
        }


    }
}