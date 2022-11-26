using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;

namespace currencyexchange_api.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly IFetchContentService _fetchContentService;

        public CurrencyRatesService(IFetchContentService fetchContentService)
        {
            _fetchContentService = fetchContentService;
        }
        public async Task<IEnumerable<CurrencyHistory>> GetRates(ExchangeSpan exchangeSpan)
        {
            var startDate = exchangeSpan.StartDate;
            var endDate = exchangeSpan.EndDate;

            List<CurrencyRate> currencyRates = new();

            foreach (var item in exchangeSpan.currencyCodes)
            {
                var gatewayRequest = new FetchCurrencyRequest(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate);

                currencyRates.AddRange(await _fetchContentService.FetchCurrencyRate(gatewayRequest));
            }

            var currencyHistory = currencyRates.Select(x => new CurrencyHistory
            {
                currency = x.Currency,
                history = new History
                {
                    date= x.Date,
                    details = new List<Details>()
                    {
                        new Details()
                        {
                            denominator = x.CurrencyDenominator,
                            rate = x.Rate,
                        }
                    }
                }
            });

            return currencyHistory;
        }
    }
}
