using currencyexchange_api;
using currencyexchange_api.Database;
using currencyexchange_api.Entity;
using currencyexchange_api.Middleware;
using currencyexchange_api.Models;
using currencyexchange_api.Services;
using currencyexchange_api.Services.Interfaces;
using currencyexchange_api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var authenticationSettings = new AuthenticationSettings();

    builder.Services.AddMemoryCache();
    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
    builder.Services.AddSingleton(authenticationSettings);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Bearer";
        options.DefaultScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
        };
    });
    builder.Services.AddDbContext<ApplicationDbContext>(c => c.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionStrings:CurrencyApi")));
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IValidator<ExchangeSpan>, ExchangeSpanValidator>();
    builder.Services.AddSingleton<ICurrencyRatesService, CurrencyRatesService>();
    builder.Services.AddSingleton<IFetchContentService, FetchContentService>();
    builder.Services.AddSingleton<IRequestResultDeserializer, RequestResultDeserializer>();
    builder.Services.AddScoped<IApiKeyGeneratorService, ApiKeyGeneratorService>();
    builder.Services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();
    builder.Services.AddHttpClient();
    builder.Services.AddScoped<ErrorHandlingMiddleware>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stopper program because of exception:{0}",e.Message);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}


