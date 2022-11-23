using currencyexchange_api.Models;
using currencyexchange_api.Services;
using currencyexchange_api.Services.Interfaces;
using Flurl.Http.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ICurrencyRatesService, CurrencyRatesService>();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
var currencyRestClientConfig = new RestClientConfig();
builder.Configuration.GetSection("RestClients:Currency").Bind(currencyRestClientConfig);
builder.Services.AddSingleton<IFetchContentService, FetchContentService>(param => new FetchContentService(currencyRestClientConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
