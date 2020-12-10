using NewsTask.Services.Enums;
using NewsTask.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsTask.Services.Contracts
{
    public interface INewsService
    {
        Task<IList<Article>> GetTopHeadlines(int pageSize, Category category, Country country);

        Task<IList<Article>> SearchArchive(string searchString, int pageSize, SortOrder sort, DateTime from, DateTime to);
    }
}