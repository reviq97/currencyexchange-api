using currencyexchange_api.Database;
using currencyexchange_api.Entity;
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

        public string GenerateApiKey(string email)
        {
            var apiuser = _applicationDbContext.ApiUsers.FirstOrDefault(x => x.Email == email);

            if (apiuser is not null)
            {
                return $"Email already contains registred apikey:{apiuser.ApiKey}";
            }

            var apiKey = Guid.NewGuid().ToString();

            _applicationDbContext.ApiUsers.Add(new ApiUser
            {
                ApiKey = apiKey,
                Email = email
            });

            _applicationDbContext.SaveChanges();
            return $"Your unique apikey is :{apiKey}\nRegistred on :{email}";
        }
    }
}
