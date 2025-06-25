using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork(TournamentContext context, ITournamentRepository tournamentRepository, IGameRepository gameRepository) : IUnitOfWork
    {

        public ITournamentRepository TournamentRepository { get; } = tournamentRepository;
        public IGameRepository GameRepository { get; } = gameRepository;

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}