using Aggregator.Models;

namespace Aggregator.Services;

public interface INewsApiService
{
	Task<NewsApiResponse> GetNewsAsync();
}
