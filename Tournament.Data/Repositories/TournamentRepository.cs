using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository(TournamentContext context) : ITournamentRepository
    {
        public void Add(TournamentDetails tournamentDetails)
        {
            context.TournamentDetails.Add(tournamentDetails);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await context.TournamentDetails.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false)
        {
            return includeGames
                ? await context.TournamentDetails.Include(t => t.Games).ToListAsync()
                : await context.TournamentDetails.ToListAsync();
        }

        public async Task<TournamentDetails> GetAsync(int id)
        {
            return await context.TournamentDetails.SingleOrDefaultAsync(t => t.Id == id);
        }

        public void Remove(TournamentDetails tournamentDetails)
        {
            context.TournamentDetails.Remove(tournamentDetails);
        }

        public void Update(TournamentDetails tournamentDetails)
        {
            context.TournamentDetails.Update(tournamentDetails);
        }
    }
}