using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace currencyexchange_api.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly ILogger<CurrencyRatesService> _logger;
        private readonly IFetchContentService _fetchContentService;
        private readonly IMemoryCache _memoryCache;

        public CurrencyRatesService(ILogger<CurrencyRatesService> logger, IFetchContentService fetchContentService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _fetchContentService = fetchContentService;
            _memoryCache = memoryCache;
        }
        public async Task<IEnumerable<IEnumerable<CurrencyHistory>>> GetRates(ExchangeSpan exchangeSpan)
        {
            _logger.LogInformation($"Inovked {nameof(ICurrencyRatesService)} interface action");
            var startDate = exchangeSpan.StartDate;
            var endDate = exchangeSpan.EndDate;

            var currencyCacheMap = new Dictionary<Tuple<string, string, DateTime, DateTime>, List<CurrencyRate>>();

            _logger.LogInformation($"Created cache map");
            foreach (var item in exchangeSpan.currencyCodes)
            {
                _logger.LogInformation("Trying to get content from memory cache");
                var currencyCache = _memoryCache.Get(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate));
                var currencyRate = new List<CurrencyRate>();

                if (currencyCache is null)
                {
                    _logger.LogInformation("Cache are empty, prepparing for a new api request");
                    var gatewayRequest = new FetchCurrencyRequest(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate);
                    var response = await _fetchContentService.FetchCurrencyRate(gatewayRequest);
                    currencyRate.AddRange(response);
                    currencyCacheMap.Add(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate), currencyRate);

                    _logger.LogInformation("Setting up memory cache with response");
                    _memoryCache.Set(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate), currencyRate);
                }
                else
                {
                    _logger.LogInformation("Adding cache content to a dictionary map");
                    currencyCacheMap.Add(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate), (List<CurrencyRate>)currencyCache);
                }

            }

            _logger.LogInformation("Projecting data");
            var historyRatesList = currencyCacheMap.ToList().Select(x => x.Value.Select(x => new CurrencyHistory
            {
                currency = x.Currency,
                history = new List<History>()
                {
                    new History
                    {
                        date = x.Date,
                        details = new Details
                        {
                            denominator = x.CurrencyDenominator,
                            rate = x.Rate,
                        }
                    }
                }
            }));

            return historyRatesList.ToList();

        }
    }
}
