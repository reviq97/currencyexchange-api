using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace currencyexchange_api.Entity
{

    public class ExchangeSpan
    {
        private IDictionary<string, string> _currencyCodes;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _apiKey;


        public string ApiKey
        {
            get { return _apiKey; }
            init { _apiKey = value; }
        }
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

        public IDictionary<string, string> currencyCodes
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
