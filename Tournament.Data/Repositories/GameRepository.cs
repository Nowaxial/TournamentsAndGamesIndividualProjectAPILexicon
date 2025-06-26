using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;

public class GameRepository(TournamentContext context) : IGameRepository
{
    public void Add(Game game)
    {
        context.Games.Add(game);
    }

    public async Task<bool> AnyAsync(int id)
    {
        return await context.Games.AnyAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await context.Games.ToListAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await context.Games.SingleOrDefaultAsync(t => t.Id == id);
    }

    public void Remove(Game game)
    {
        context.Games.Remove(game);
    }

    public void Update(Game game)
    {
        context.Games.Update(game);
    }
}