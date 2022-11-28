

using currencyexchange_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace currencyexchange_api_tests
{
    public class ApiKeyGeneratorServiceTests : IClassFixture<WebApplicationFactory<Program>>
    {
        HttpClient _client;
        private Mock<IApiKeyGeneratorService> _apiKeyGeneratorService = new Mock<IApiKeyGeneratorService>();
        public ApiKeyGeneratorServiceTests(WebApplicationFactory<StartUp> fac)
        {
            _client = fac.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContext = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContext);

                });
            }).CreateClient();
        }

        [Theory]
        [InlineData("nway@o2.pl")]
        public void GenerateApiKey_ForExistingMailInDatabase_ReturnsApiKey(string mail)
        {

            //arrange
            var apiKey = "ce969623-2ca1-4cb1-8f22-e9b482795765";
            _apiKeyGeneratorService.Setup(x => x.GenerateApiKey(mail)).Returns(apiKey);

        }
    }
}