namespace currencyexchange_api.Entity
{
    public class ExchangeSpan
    {
		private Dictionary<string,string>? _currencyCodes;
		private DateTime _startDate;
		private DateTime _endDate;

		public DateTime EndDate
		{
			get { return _endDate; }
			init { _endDate = value; }
		}

		public DateTime StartDate
		{
			get { return _startDate; }
			init { _startDate = value; }
		}

		public Dictionary<string,string> currencyCodes
		{
			get
			{
				return _currencyCodes ?? new Dictionary<string, string>();
			}

			init { _currencyCodes = value; }
		}

		
	}
}
