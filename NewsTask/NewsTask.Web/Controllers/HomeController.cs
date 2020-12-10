using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsTask.Data.Enums;
using NewsTask.Services.Contracts;
using NewsTask.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NewsTask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsService _newsService;

        public HomeController(ILogger<HomeController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public async Task<IActionResult> Index(int pageSize, Category category, Country country)
        {
            if (pageSize == 0)
                pageSize = 20;

            var listArticles = await _newsService.GetTopHeadlines(pageSize, category, country);

            var listArticlevVM = listArticles.Adapt<IEnumerable<ArticleVM>>();

            return View(listArticlevVM);
        }

        public IActionResult Archive()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}