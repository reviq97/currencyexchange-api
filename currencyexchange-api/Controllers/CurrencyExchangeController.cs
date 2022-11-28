using currencyexchange_api.Entity;
using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace currencyexchange_api.Controllers
{
    [Route("api/currencyexchange")]
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyRatesService _currencyRatesService;
        private readonly IApiKeyGeneratorService _apiKeyGeneratorService;
        private readonly IJwtGeneratorService _jwtGeneratorService;
        private readonly ILogger<CurrencyExchangeController> _logger;

        public CurrencyExchangeController(ICurrencyRatesService currencyExchangeService, 
                                          IApiKeyGeneratorService apiKeyGeneratorService, 
                                          IJwtGeneratorService jwtGeneratorService,
                                          ILogger<CurrencyExchangeController> logger)
        {
            _currencyRatesService = currencyExchangeService;
            _apiKeyGeneratorService = apiKeyGeneratorService;
            _jwtGeneratorService = jwtGeneratorService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("currency/timeline")]
        public async Task<ActionResult<IEnumerable<CurrencyHistory>>> GetRates([FromBody] ExchangeSpan exchangeSpan)
        {
            _logger.LogInformation($"Invoked {nameof(GetRates)} controller");
            var result = await _currencyRatesService.GetRates(exchangeSpan);
            _logger.LogInformation($"Retrieve result to controller:currency/timeline");
            return Ok(result);
        }

        [HttpPost("apikey/register")]
        public async Task<ActionResult<string>> RegisterApiKey(string email)
        {
            _logger.LogInformation($"Invoked apikey/register controller");
            var message = _apiKeyGeneratorService.GenerateApiKey(email);

            return Ok(message);
        }

        [HttpPost("apikey/login")]
        public async Task<ActionResult<string>> Login(string email)
        {
            _logger.LogInformation($"Invoked apikey/login controller");
            var token = _jwtGeneratorService.GenerateJwtToken(email);

            return Ok(token);
        }

    }
}
