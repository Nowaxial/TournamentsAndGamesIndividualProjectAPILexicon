using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public class TournamentContext(DbContextOptions<TournamentContext> options) : DbContext(options)
    {
        public DbSet<TournamentDetails> TournamentDetails { get; set; } = default!;
        public DbSet<Game> Game { get; set; } = default!;
    }
}