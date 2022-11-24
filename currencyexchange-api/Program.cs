using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services;
using currencyexchange_api.Services.Interfaces;
using currencyexchange_api.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Flurl.Http.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSingleton<ICurrencyRatesService, CurrencyRatesService>();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
var currencyRestClientConfig = new RestClientConfig();
builder.Configuration.GetSection("RestClients:Currency").Bind(currencyRestClientConfig);
builder.Services.AddSingleton<IFetchContentService, FetchContentService>(param => new FetchContentService(currencyRestClientConfig));
builder.Services.AddScoped<IValidator<ExchangeSpan>, ExchangeSpanValidator>();

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
