namespace currencyexchange_api.Models
{
    public class CurrencyHistory
    {
        public string currency { get; init; }
        public List<History> history { get; init; }
    }
    public class Details
    {
        public string denominator { get; init; }
        public string rate { get; init; }
    }

    public class History
    {
        public DateTime date { get; init; }
        public List<Details> details { get; init; }
    }
    
}
