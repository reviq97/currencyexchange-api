using currencyexchange_api.Database;
using currencyexchange_api.Entity;
using currencyexchange_api.Services.Interfaces;

namespace currencyexchange_api.Services
{
    public class ApiKeyGeneratorService : IApiKeyGeneratorService
    {
        private readonly ILogger<ApiKeyGeneratorService> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public ApiKeyGeneratorService(ILogger<ApiKeyGeneratorService> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public string GenerateApiKey(string email)
        {
            _logger.LogInformation($"Checking if email={email} is already in database");
            var apiuser = _applicationDbContext.ApiUsers.FirstOrDefault(x => x.Email == email);

            if (apiuser is not null)
            {
                return $"Email already contains registred apikey:{apiuser.ApiKey}";
            }

            var apiKey = Guid.NewGuid().ToString();
            _logger.LogInformation($"Creating new apikey {apiKey}");

            _applicationDbContext.ApiUsers.Add(new ApiUser
            {
                ApiKey = apiKey,
                Email = email
            });
            _logger.LogInformation("Posting new apiuser");

            _applicationDbContext.SaveChanges();
            _logger.LogInformation("Saving apiuser in database");
            return $"Your unique apikey is :{apiKey}\nRegistred on :{email}";
        }
    }
}
