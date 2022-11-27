namespace currencyexchange_api.Models
{
    public class FetchCurrencyRequest
    {
        private string _currency;
        private string _currencyDenominator;
        private DateTime _startDate;
        private DateTime _endDate;

        public FetchCurrencyRequest(string currency, string currencyDenominator, DateTime startDate, DateTime endDate)
        {
            _currency = currency;
            _currencyDenominator = currencyDenominator;
            _startDate = startDate;
            _endDate = endDate;
        }

        public string Currency { get => _currency; init => _currency = value; }
        public string CurrencyDenominator { get => _currencyDenominator; init => this._currencyDenominator = value; }
        public DateTime StartDate { get => _startDate; init => _startDate = value; }
        public DateTime EndDate { get => _endDate; init => _endDate = value; }
    }
}