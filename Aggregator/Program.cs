using Aggregator.Services;

using NewsApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INewsApiService, NewsApiService>();

builder.Services.AddGrpcClient<NewsApiGrpc.NewsApiGrpcClient>((services, options) =>
{
	options.Address = new Uri("https://localhost:7087");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
