using currencyexchange_api.Entity;
using currencyexchange_api.Models;

namespace currencyexchange_api.Services.Interfaces
{
    public interface ICurrencyRatesService
    {
        Task<IEnumerable<IEnumerable<CurrencyHistory>>> GetRates(ExchangeSpan exchangeSpan);
    }
}