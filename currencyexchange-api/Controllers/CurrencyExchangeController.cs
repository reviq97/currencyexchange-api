using currencyexchange_api.Entity;
using currencyexchange_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace currencyexchange_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyRatesService _currencyExchangeService;

        public CurrencyExchangeController(ICurrencyRatesService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }

        [HttpPost]
        public async Task<object> GetRates([FromBody] ExchangeSpan exchangeSpan)
        {
            var result = await _currencyExchangeService.GetRates(exchangeSpan);

            return result;
        }

    }
}
