using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using Flurl;
using Flurl.Http;

namespace currencyexchange_api.Services
{
    public class FetchContentService : IFetchContentService
    {
        private readonly RestClientConfig _clientConfig;

        public FetchContentService(RestClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public async Task<CurrencyRate> FetchCurrencyRate(FetchCurrencyRequest currencyRequest)
        {
            var currency = currencyRequest.Currency;
            var currencyDenominator = currencyRequest.CurrencyDenominator;

            var clientRequest = await (_clientConfig.BasePath + $@"D.{currency}.{currencyDenominator}.SP00.A").SetQueryParams(new
            {
                startDate = currencyRequest.StartDate.ToString("yyyy-MM-dd"),
                endDate = currencyRequest.EndDate.ToString("yyyy-MM-dd")
            }).GetJsonAsync();

            //TODO:Map response to the list of objects and return results 

            return null;
        }
    }
}
