namespace Aggregator.Models;

public class NewsApiResponse
{
	public string Status { get; set; }
	public int? TotalResults { get; set; }
	public List<ArticleDto>? Articles { get; set; }
	public string? Code { get; set; }
	public string? Message { get; set; }
}
