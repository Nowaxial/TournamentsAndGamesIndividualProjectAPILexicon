﻿using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false);
        Task<TournamentDetails?> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(TournamentDetails tournamentDetails);
        void Update(TournamentDetails tournamentDetails);
        void Remove(TournamentDetails tournamentDetails);
    }
}