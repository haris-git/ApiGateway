namespace NewsApiConsumer.Model
{
	public class NewsItem
	{
		public string? status { get; set; }
		public int? totalResults { get; set; }
		public List<Article>? articles { get; set; }
		public string? code { get; set; }
		public string? message { get; set; }
	}
}
