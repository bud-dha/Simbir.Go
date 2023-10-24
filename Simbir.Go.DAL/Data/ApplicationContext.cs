using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.DAL.Data
{
    public class ApplicationContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Transport> Transports { get; set; } = null!;
        public DbSet<Rent> Rents { get; set; } = null!;

        public ApplicationContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("ApiDatabase"));
        }
    }
}