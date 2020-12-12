using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsTask.Data.Enums;
using NewsTask.Services.Contracts;
using NewsTask.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewsTask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsService _newsService;
        private static List<ArticleVM> articlesTest = new List<ArticleVM>();

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

            var listArticlesVM = listArticles.Adapt<IEnumerable<ArticleVM>>();

            articlesTest = listArticlesVM.ToList();

            return View(listArticlesVM);
        }

        public async Task<IActionResult> Archive(string searchString, int pageSize, SortOrder sort, DateTime from, DateTime to)
        {

            var listArticles = await _newsService.SearchArchive(searchString, pageSize, sort, from, to);

            var listArticlesVM = listArticles.Adapt<IEnumerable<ArticleVM>>();

            return View(listArticlesVM);
        }
        
        public IActionResult Excel()
        {

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("News");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Title";
                worksheet.Cell(currentRow, 2).Value = "Description";
                worksheet.Cell(currentRow, 3).Value = "Source";
                worksheet.Cell(currentRow, 4).Value = "Url";
                worksheet.Cell(currentRow, 5).Value = "UrlToImage";
                worksheet.Cell(currentRow, 6).Value = "Published";
                worksheet.Cell(currentRow, 7).Value = "Content";

                foreach (var article in articlesTest)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = article.Title;
                    worksheet.Cell(currentRow, 2).Value = article.Description;
                    worksheet.Cell(currentRow, 3).Value = article.Source.Name;
                    worksheet.Cell(currentRow, 4).Value = article.Url;
                    worksheet.Cell(currentRow, 5).Value = article.UrlToImage;
                    worksheet.Cell(currentRow, 6).Value = article.PublishedAt;
                    worksheet.Cell(currentRow, 7).Value = article.Content;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "news.xlsx");
                }

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}