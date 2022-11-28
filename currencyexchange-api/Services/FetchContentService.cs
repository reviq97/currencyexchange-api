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
        private readonly ILogger<FetchContentService> _logger;
        private readonly IConfiguration _configuration;

        public FetchContentService(ILogger<FetchContentService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory, IRequestResultDeserializer requestResultSerializer)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _requestResultDeserializer = requestResultSerializer;
        }

        public async Task<IEnumerable<CurrencyRate>> FetchCurrencyRate(FetchCurrencyRequest currencyRequest)
        {
            _logger.LogInformation($"Invoked {nameof(IFetchContentService)} interface");
            var basePath = _configuration.GetValue<string>("RestClients:ExchangeRate:BasePath");
            var currency = currencyRequest.Currency;
            var currencyDenominator = currencyRequest.CurrencyDenominator;
            var startDate = currencyRequest.StartDate.ToString("yyyy-MM-dd");
            var endDate = currencyRequest.EndDate.ToString("yyyy-MM-dd");

            _logger.LogInformation($"Preparing uri for request");
            var uri = new Uri(basePath + $"timeseries?start_date={currencyRequest.StartDate.ToString("yyyy-MM-dd")}" +
                                         $"&end_date={currencyRequest.EndDate.ToString("yyyy-MM-dd")}" +
                                         $"&base={currencyRequest.Currency}" +
                                         $"&symbols={currencyRequest.CurrencyDenominator}" +
                                         $"&amount=1&format=xml");

            _logger.LogInformation($"Uri={uri.ToString()} ready for request");
            _logger.LogInformation("Creating HttpClientFactory");
            var httpClient = _httpClientFactory.CreateClient();

            _logger.LogInformation($"Calling api for a {uri}");
            var response = await httpClient.GetStringAsync(uri);

            _logger.LogInformation($"Response: {response}");
            var parseResult = await _requestResultDeserializer.DeserializeXmlToObject<document>(response);
            _logger.LogInformation("Object deserializing finished");

            var currencyRateList = parseResult.data.Select(x => new CurrencyRate
            {
                Currency = x.@base,
                CurrencyDenominator = x.code,
                Rate = x.rate.ToString(),
                Date = x.date,
            });
            _logger.LogInformation($"Projecting to a {nameof(CurrencyRate)} object");

            return currencyRateList;
        }
    }
}
