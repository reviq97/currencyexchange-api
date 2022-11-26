using currencyexchange_api.Database;
using currencyexchange_api.Services.Interfaces;

namespace currencyexchange_api.Services
{
    public class ApiKeyGeneratorService : IApiKeyGeneratorService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ApiKeyGeneratorService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public string GenerateApiKey()
        {
            var apiKey = Guid.NewGuid().ToString();


            return apiKey;
        }
    }
}
