namespace currencyexchange_api.Services.Interfaces
{
    public interface IJwtGeneratorService
    {
        string GenerateJwtToken(string email);
    }
}