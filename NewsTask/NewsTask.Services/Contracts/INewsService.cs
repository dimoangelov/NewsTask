using System.Threading.Tasks;

namespace NewsTask.Services.Contracts
{
    public interface INewsService
    {
        Task GetTopHeadlines();
    }
}