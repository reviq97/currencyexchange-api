using currencyexchange_api.Database;
using currencyexchange_api.Entity;
using FluentValidation;

namespace currencyexchange_api.Validation
{
    public class ExchangeSpanValidator : AbstractValidator<ExchangeSpan>
    {
        public ExchangeSpanValidator(ApplicationDbContext applicationDbContext) 
        {
            RuleFor(curr => curr.currencyCodes).Must(dict =>
            {
                foreach (var item in dict)
                {
                    if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Value) || string.IsNullOrWhiteSpace(item.Key) || string.IsNullOrWhiteSpace(item.Value) || item.Key.Length != 3 || item.Value.Length != 3)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Currencies cannot be empty");

            RuleFor(apiKey => apiKey.ApiKey).Must(apiKey => applicationDbContext.ApiUsers.Any(x => x.ApiKey == apiKey)).WithMessage("Wring api-key");
            RuleFor(endDate => endDate.EndDate).Must(endDate => endDate.Date <= DateTime.Now || !string.IsNullOrEmpty(endDate.ToString()) || !string.IsNullOrWhiteSpace(endDate.ToString())).WithMessage("Date couldn't be higher than today or empty");
            RuleFor(startDate => startDate.EndDate).Must(startDate => startDate.Date <= DateTime.Now || !string.IsNullOrEmpty(startDate.ToString()) || !string.IsNullOrWhiteSpace(startDate.ToString())).WithMessage("Date couldn't be higher than today or empty");
            RuleFor(date => new { date.StartDate, date.EndDate }).Must(date => date.StartDate.Date <= date.EndDate.Date).WithMessage("Start date should be earlier than End date");
        }
    }
}
