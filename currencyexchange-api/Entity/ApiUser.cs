using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace currencyexchange_api.Entity
{
    [Table("api_users")]
    public class ApiUser
    {
        private string _apiKey;
        private string _email;

        [Required]
        [Key]
        [Column("api_key")]
        public string ApiKey
        {
            get { return _apiKey; }
            init { _apiKey = value; }
        }

        [Required]
        [Column("email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
