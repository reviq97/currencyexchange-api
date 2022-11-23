using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using Flurl.Http;
using System.Runtime.CompilerServices;

namespace currencyexchange_api.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly IFetchContentService _fetchContentService;

        public CurrencyRatesService(IFetchContentService fetchContentService)
        {
            _fetchContentService = fetchContentService;
        }
        public async Task<IEnumerable<CurrencyRate>> GetRates(ExchangeSpan exchangeSpan)
        {
            var startDate = exchangeSpan.StartDate;
            var endDate = exchangeSpan.EndDate;
            var dictionary = exchangeSpan.currencyCodes;

            List<CurrencyRate> currencyRates = new();

            foreach (var item in dictionary)
            {
                var gatewayRequest = new FetchCurrencyRequest(item.Key, item.Value, startDate, endDate);

                var currencyContent =  await _fetchContentService.FetchCurrencyRate(gatewayRequest);
                
            }

            return null;
        }
    }
}
