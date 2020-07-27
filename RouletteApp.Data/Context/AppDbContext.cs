using Microsoft.EntityFrameworkCore;
using RouletteApp.Data.Emtities;

namespace RouletteApp.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Roulette> Roulette { get; set; }
        public DbSet<Consumer> Consumer { get; set; }
        public DbSet<Bet> Bet { get; set; }
    }
}