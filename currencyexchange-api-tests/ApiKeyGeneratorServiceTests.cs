

namespace currencyexchange_api_tests
{
    public class ApiKeyGeneratorServiceTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        HttpClient _client;
        private Mock<IApiKeyGeneratorService> _apiKeyGeneratorService = new Mock<IApiKeyGeneratorService>();
        public ApiKeyGeneratorServiceTests(WebApplicationFactory<StartUp> fac)
        {
            _client = fac.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IApiKeyGeneratorService>(_apiKeyGeneratorService.Object);

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