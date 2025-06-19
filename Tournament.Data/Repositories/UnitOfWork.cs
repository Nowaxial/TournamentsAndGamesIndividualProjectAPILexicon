using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;

public class UnitOfWork(TournamentContext context) : IUnitOfWork
{
    public IGameRepository GameRepository => new GameRepository(context);
    public ITournamentRepository TournamentRepository => new TournamentRepository(context);
    public async Task CompleteAsync() => await context.SaveChangesAsync();
}