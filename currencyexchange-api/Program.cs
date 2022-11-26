using currencyexchange_api.Database;
using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services;
using currencyexchange_api.Services.Interfaces;
using currencyexchange_api.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<ExchangeSpan>, ExchangeSpanValidator>();
builder.Services.AddSingleton<ICurrencyRatesService, CurrencyRatesService>();
builder.Services.AddSingleton<IFetchContentService, FetchContentService>();
builder.Services.AddSingleton<IRequestResultDeserializer, RequestResultSerializer>();
builder.Services.AddScoped<IApiKeyGeneratorService, ApiKeyGeneratorService>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ApplicationDbContext>(c => c.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionStrings:CurrencyApi")));

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
