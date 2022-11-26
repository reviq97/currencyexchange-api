using currencyexchange_api.Entity;
using Microsoft.EntityFrameworkCore;

namespace currencyexchange_api.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        DbSet<ExchangeSpan> ExchangeSpans { get; set; }
    }
}