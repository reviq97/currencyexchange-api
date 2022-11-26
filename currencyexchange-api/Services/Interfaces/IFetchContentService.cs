using currencyexchange_api.Models;

namespace currencyexchange_api.Services.Interfaces
{
    public interface IFetchContentService
    {
        Task<IEnumerable<CurrencyRate>> FetchCurrencyRate(FetchCurrencyRequest currencyRequest);
    }
}