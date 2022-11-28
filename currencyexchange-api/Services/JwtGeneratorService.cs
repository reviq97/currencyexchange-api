using currencyexchange_api.Database;
using currencyexchange_api.Models;
using currencyexchange_api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace currencyexchange_api.Services
{
    public class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly ILogger<JwtGeneratorService> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly AuthenticationSettings _authenticationSettings;

        public JwtGeneratorService(ILogger<JwtGeneratorService> logger, ApplicationDbContext applicationDbContext, AuthenticationSettings authenticationSettings)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwtToken(string email)
        {
            _logger.LogInformation($"Checking if email={email} is already in database");
            var apiUser = _applicationDbContext.ApiUsers.FirstOrDefault(x => x.Email == email);

            if (apiUser is null)
            {
                return "Invalid email value";
            }

            _logger.LogInformation("Creating claims");
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, apiUser.ApiKey),
                new Claim(ClaimTypes.Email, apiUser.Email)
            };

            _logger.LogInformation("Creating symetricKey, credentials, expires and generating JWTToken");
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var jwtToken = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                                                _authenticationSettings.JwtIssuer,
                                                claims,
                                                expires: expires,
                                                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            _logger.LogInformation("Returning created token");

            return tokenHandler.WriteToken(jwtToken);

        }
    }
}
