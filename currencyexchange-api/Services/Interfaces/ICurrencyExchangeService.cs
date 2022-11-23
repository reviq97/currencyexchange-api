using currencyexchange_api.Entity;

namespace currencyexchange_api.Services.Interfaces
{
    public interface ICurrencyExchangeService+

    {
        async Task<object> ExchangeCurrencies(ExchangeSpan exchangeSpan);
    }
}