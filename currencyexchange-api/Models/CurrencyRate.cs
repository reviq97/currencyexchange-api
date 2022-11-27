namespace currencyexchange_api.Models
{
    public class CurrencyRate
    {
        private string _currency;
        private string _currencyDenominator;
        private string _rate;
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            init { _date = value; }
        }

        public string Rate
        {
            get { return _rate; }
            init { _rate = value; }
        }

        public string CurrencyDenominator
        {
            get { return _currencyDenominator; }
            init { _currencyDenominator = value; }
        }

        public string Currency
        {
            get { return _currency; }
            init { _currency = value; }
        }

    }
}
