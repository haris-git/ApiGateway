using System.Text.Json;
using Aggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers;

[ApiController]
public class AggregatorController : ControllerBase
{
	private readonly INewsApiService _newsApiService;

	public AggregatorController(INewsApiService newsApiService)
	{
		_newsApiService = newsApiService;
	}

	[HttpGet("api/v1/AggregateResultsFromApis")]
	public async Task<IActionResult> AggregateResultsFromApis(string? inputParametersForGettingAggregatedResults) //TODO Use inputParametersForGettingAggregatedResults to create some cases.
	{
		//Various conditions will be here, depending on inputParametersForGettingAggregatedResults...
		// ....
		// ....
		// ....

		//If the user required some info about the news, we should call the NewsApiConsumer. TODO Move this inside an if statement.
		var newsApiResult = await _newsApiService.GetNewsAsync();

		return Ok(JsonSerializer.Serialize(newsApiResult));
	}
}
