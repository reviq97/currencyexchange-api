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
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly AuthenticationSettings _authenticationSettings;

        public JwtGeneratorService(ApplicationDbContext applicationDbContext, AuthenticationSettings authenticationSettings)
        {
            _applicationDbContext = applicationDbContext;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwtToken(string email)
        {
            var apiUser = _applicationDbContext.ApiUsers.FirstOrDefault(x => x.Email == email);

            if (apiUser is null) 
            {
                return "Invalid email value";
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, apiUser.ApiKey),
                new Claim(ClaimTypes.Email, apiUser.Email)
            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var jwtToken = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                                                _authenticationSettings.JwtIssuer,
                                                claims,
                                                expires: expires,
                                                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(jwtToken);

        }
    }
}
