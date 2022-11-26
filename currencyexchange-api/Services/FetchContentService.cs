using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using System.Text;

namespace currencyexchange_api.Services
{
    public class FetchContentService : IFetchContentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRequestResultDeserializer _requestResultDeserializer;
        private readonly IConfiguration _configuration;

        public FetchContentService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IRequestResultDeserializer requestResultSerializer)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _requestResultDeserializer = requestResultSerializer;
        }

        public async Task<IEnumerable<CurrencyRate>> FetchCurrencyRate(FetchCurrencyRequest currencyRequest )
        {

            var basePath = _configuration.GetValue<string>("RestClients:ExchangeRate:BasePath");
            var currency = currencyRequest.Currency;
            var currencyDenominator = currencyRequest.CurrencyDenominator;
            var startDate = currencyRequest.StartDate.ToString("yyyy-MM-dd");
            var endDate = currencyRequest.EndDate.ToString("yyyy-MM-dd");

            var uri = new Uri(basePath + $"timeseries?start_date={currencyRequest.StartDate.ToString("yyyy-MM-dd")}" +
                                         $"&end_date={currencyRequest.EndDate.ToString("yyyy-MM-dd")}" +
                                         $"&base={currencyRequest.Currency}" +
                                         $"&symbols={currencyRequest.CurrencyDenominator}" +
                                         $"&amount=1&format=xml");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync(uri);
            var parseResult = await _requestResultDeserializer.DeserializeXmlToObject<document>(response);

            var currencyRateList = parseResult.data.Select(x => new CurrencyRate
            {
                Currency = x.@base,
                CurrencyDenominator = x.code,
                Rate = x.rate.ToString(),
                Date = x.date,
            });


            return currencyRateList;
        }
    }
}
