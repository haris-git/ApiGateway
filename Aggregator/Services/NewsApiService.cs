using Aggregator.Models;
using NewsApi;
using System;

namespace Aggregator.Services;

public class NewsApiService : INewsApiService
{
	private readonly NewsApiGrpc.NewsApiGrpcClient _client;

	public NewsApiService(NewsApiGrpc.NewsApiGrpcClient client)
	{
		_client = client;
	}

	public async Task<NewsApiResponse> GetNewsAsync()
	{
		GetNewsRequest request = new()
		{
			Q = "microsoft" //TODO refactor to get this parameter from the uri.
		};

		GetNewsResponse response = await _client.GetNewsAsync(request);

		var newsApiResponse =  new NewsApiResponse()
		{
			Status = response.Status,
			TotalResults = response.TotalResults,
			Articles = new List<ArticleDto>(),
			Code = response.Code,
			Message = response.Message,
		};

		foreach (var articleApiResponse in response.Articles)
		{
			var tempArticle = new ArticleDto()
			{
				Author = articleApiResponse.Author,
				Title = articleApiResponse.Title,
				Description = articleApiResponse.Description,
				Url = articleApiResponse.Url,
				UrlToImage = articleApiResponse.UrlToImage,
				PublishedAt = articleApiResponse.PublishedAt,
				Content = articleApiResponse.Content
			};

			newsApiResponse.Articles.Add(tempArticle);
		}
		
		return newsApiResponse;
	}
}
