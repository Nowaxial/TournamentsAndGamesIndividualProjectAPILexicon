using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository(TournamentContext context) : ITournamentRepository
    {
        public void Add(TournamentDetails tournament) => context.TournamentDetails.Add(tournament);

        public async Task<bool> AnyAsync(int id) => await Task.FromResult(context.TournamentDetails.Any(t => t.Id == id));

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync() => await Task.FromResult(context.TournamentDetails.ToList());

        public Task<TournamentDetails> GetAsync(int id) => Task.FromResult(context.TournamentDetails.SingleOrDefault(t => t.Id == id));

        public void Remove(TournamentDetails tournament) => context.TournamentDetails.Remove(tournament);

        public void Update(TournamentDetails tournament) => context.TournamentDetails.Update(tournament);
    }
}
