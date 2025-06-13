using Semana_api_11.Models;
using Microsoft.EntityFrameworkCore;

namespace Semana_api_11.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Customer> Customer => Set<Customer>();
        public DbSet<Product> Product => Set<Product>();

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }
    }
}
