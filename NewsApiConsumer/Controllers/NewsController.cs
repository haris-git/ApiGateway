using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace NewsApiConsumer.Controllers;

[ApiController]
public class NewsController : ControllerBase
{
	private readonly HttpClient _httpClient;

	public NewsController(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET 8 IHttpClientFactory");
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", "3e3a0b42ac544a25b9c64f7cea25ccce");// TODO Move this to appsettings.json
    }

	[HttpGet("api/v1/news")]
	public async Task<IActionResult> GetNewsAsync(CancellationToken cancellationToken)
	{
		var url = "https://newsapi.org/v2/everything?q=tesla&from=2024-01-19&sortBy=publishedAt";
		try
		{
			var response = await _httpClient.GetStringAsync(url, cancellationToken: cancellationToken);
            return Ok(response);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}
