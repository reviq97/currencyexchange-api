using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace currencyexchange_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyRatesService _currencyRatesService;
        private readonly IApiKeyGeneratorService _apiKeyGeneratorService;

        public CurrencyExchangeController(ICurrencyRatesService currencyExchangeService)
        {
            _currencyRatesService = currencyExchangeService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IEnumerable<CurrencyHistory>> GetRates([FromBody] ExchangeSpan exchangeSpan)
        {
            var result = await _currencyRatesService.GetRates(exchangeSpan);

            return result;
        }

        [HttpGet]
        public async Task<string> GetApiKey()
        {
            var apiKey = _apiKeyGeneratorService.GenerateApiKey();
            return apiKey;
        }

    }
}
