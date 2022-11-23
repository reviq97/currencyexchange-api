using currencyexchange_api.Models;

namespace currencyexchange_api.Services.Interfaces
{
    public interface IFetchContentService
    {
        Task<CurrencyRate> FetchCurrencyRate(FetchCurrencyRequest currencyRequest);
    }
}