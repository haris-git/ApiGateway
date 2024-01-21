using Grpc.Core;
using NewsApi;
using System.Text.Json;
using Google.Protobuf.Collections;
using NewsApiConsumer.Model;
using static System.Net.WebRequestMethods;
using System;

namespace NewsApiConsumer.Services;

public class NewsApiService : NewsApiGrpc.NewsApiGrpcBase
{
	private readonly HttpClient _httpClient;

	public NewsApiService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient();
		_httpClient.DefaultRequestHeaders.Add("X-Api-Key", "3e3a0b42ac544a25b9c64f7cea25ccce");// TODO Move this to appsettings.json
	}
	public override async Task<GetNewsResponse> GetNews(GetNewsRequest request, ServerCallContext context)
	{
		var url = $"https://newsapi.org/v2/everything?q={request.Q}&from=2024-01-19&sortBy=publishedAt";
		NewsItem? deserializedResponse = null;
		
		try
		{
			deserializedResponse = await _httpClient.GetFromJsonAsync<NewsItem>(url);
		}
		catch (Exception e)
		{
			throw new RpcException(new Status(StatusCode.Unavailable, e.Message));
		}
		
		if (deserializedResponse == null)
		{
			throw new RpcException(new Status(StatusCode.Unavailable, "Unavailable NewsApi."));
		}

		if (deserializedResponse.status == "error")
		{
			return new GetNewsResponse()
			{
				Status = deserializedResponse?.status ?? string.Empty,
				TotalResults = 0,
				Code = deserializedResponse?.code ?? string.Empty,
				Message = deserializedResponse?.message ?? string.Empty,
			};
		}

		var articles = new RepeatedField<ArticlesResponse>();
		foreach (var article in deserializedResponse.articles)
		{
			var  tempArticle = new ArticlesResponse()
			{
				Source = new SourceResponse()
				{
					Id = article.source?.id ?? "",
					Name = article.source?.name ?? ""
				},
				Author = article?.author ?? "",
				Title = article?.title ?? "",
				Description = article?.description ?? "",
				Url = article?.url ?? "",
				UrlToImage = article?.urlToImage ?? "",
				PublishedAt = article?.publishedAt ?? "",
				Content = article?.content ?? "",
			};

			articles.Add(tempArticle);
		}
		
		var returnResult = new GetNewsResponse
		{
			Status = deserializedResponse?.status ?? string.Empty,
			TotalResults = deserializedResponse?.totalResults ?? 0,
			Code = string.Empty,
			Message = string.Empty
		};
		returnResult.Articles.AddRange(articles);

		return returnResult;
	}
}
