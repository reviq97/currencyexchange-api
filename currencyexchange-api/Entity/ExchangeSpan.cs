using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace currencyexchange_api.Entity
{
	[Table("api_users")]
    public class ExchangeSpan
    {
		private IDictionary<string, string> _currencyCodes;
		private DateTime _startDate;
		private DateTime _endDate;
		private string _apiKey;

		[Required]
		[Key]
		[Column("api_key")]
		public string ApiKey
		{
			get { return _apiKey; }
			init { _apiKey = value; }
		}
		[NotMapped]
		public DateTime EndDate
		{
			get { return _endDate; }
			init { _endDate = value; }
		}
		[NotMapped]
		public DateTime StartDate
		{
			get { return _startDate; }
			init { _startDate = value; }
		}

		[NotMapped]
		public IDictionary<string,string> currencyCodes
		{
			get
			{
				return _currencyCodes;
			}

			init 
			{ 
				_currencyCodes = value; 
			}
		}

		
	}
}
