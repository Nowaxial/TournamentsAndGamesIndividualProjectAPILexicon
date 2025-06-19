using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;

public class GameRepository(TournamentContext context) : IGameRepository
{
    public void Add(Game game) => context.Game.Add(game);
    public async Task<bool> AnyAsync(int id) => await Task.FromResult(context.Game.Any(t => t.Id == id));
    public async Task<IEnumerable<Game>> GetAllAsync() => await Task.FromResult(context.Game.ToList());
    public async Task<Game> GetAsync(int id) => await Task.FromResult(context.Game.Find(id));
    public void Remove(Game game) => context.Game.Remove(game);
    public void Update(Game game) => context.Game.Update(game);
}