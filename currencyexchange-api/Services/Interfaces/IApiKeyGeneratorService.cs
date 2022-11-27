namespace currencyexchange_api.Services.Interfaces
{
    public interface IApiKeyGeneratorService
    {
        string GenerateApiKey(string email);
    }
}