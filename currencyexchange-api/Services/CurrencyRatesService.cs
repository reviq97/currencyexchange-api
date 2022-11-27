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
        private readonly IFetchContentService _fetchContentService;
        private readonly IMemoryCache _memoryCache;

        public CurrencyRatesService(IFetchContentService fetchContentService, IMemoryCache memoryCache)
        {
            _fetchContentService = fetchContentService;
            _memoryCache = memoryCache;
        }
        public async Task<IEnumerable<CurrencyHistory>> GetRates(ExchangeSpan exchangeSpan)
        {
            var startDate = exchangeSpan.StartDate;
            var endDate = exchangeSpan.EndDate;

            var currencyCacheMap = new Dictionary<Tuple<string, string, DateTime, DateTime >, List<CurrencyRate>>();


            foreach (var item in exchangeSpan.currencyCodes)
            {
                var currencyCache = _memoryCache.Get(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(),startDate, endDate ));
                var currencyRate = new List<CurrencyRate>();

                if (currencyCache is null)
                {

                    var gatewayRequest = new FetchCurrencyRequest(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate);
                    var response = await _fetchContentService.FetchCurrencyRate(gatewayRequest);
                    currencyRate.AddRange(response);
                    currencyCacheMap.Add(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate), currencyRate);
                    _memoryCache.Set(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate), currencyRate);
                }
                else
                {
                    currencyCacheMap.Add(new Tuple<string, string, DateTime, DateTime>(item.Key.ToUpper(), item.Value.ToUpper(), startDate, endDate), (List<CurrencyRate>)currencyCache);
                }

            }

            //TODO:Convert dictionary to proper results IEnumerable<CurrencyHistory>
            var x = currencyCacheMap;

            return null;
        }
    }
}
