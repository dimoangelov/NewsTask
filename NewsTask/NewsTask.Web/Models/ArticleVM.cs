using NewsTask.Services.Enums;
using NewsTask.Services.Models;
using System;

namespace NewsTask.Web.Models
{
    public class ArticleVM
    {
        public Source Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTime PublishedAt { get; set; }

        public string Content { get; set; }

        //public Country Country { get; set; }

        //public Category Category { get; set; }
    }
}
