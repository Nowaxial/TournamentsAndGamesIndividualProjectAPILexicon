using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public static class SeedData
    {
        public static async Task Initialize(TournamentContext context)
        {
            await context.Database.MigrateAsync();

            if (!context.TournamentDetails.Any())
            {
                var tournaments = new[]
                {
                    CreateTournament("Vintercupen 2025", 2025, 1),
                    CreateTournament("Vårcupen 2025", 2025, 3),
                    CreateTournament("Sommarturneringen", 2025, 6),
                    CreateTournament("Höstturneringen", 2025, 9),
                };

                if (!tournaments.All(ValidateTournamentGames))
                {
                    throw new InvalidOperationException("Invalid games for one or more tournaments");
                }

                context.TournamentDetails.AddRange(tournaments);
                await context.SaveChangesAsync();
            }
        }

        private static TournamentDetails CreateTournament(string title, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(3);

            return new TournamentDetails
            {
                Title = title,
                StartDate = startDate,
                Games = new[]
                {
                    new Game { Title = $"{title} Gruppspel", Time = startDate.AddMonths(1).AddDays(10) },
                    new Game { Title = $"{title} Öppningsmatch", Time = startDate.AddMonths(1).AddDays(8) },
                    new Game { Title = $"{title} Final", Time = startDate.AddMonths(2).AddDays(16) },
                    new Game { Title = $"{title} Semifinal", Time = startDate.AddMonths(2).AddDays(15) },
                },
            };
        }

        private static bool ValidateTournamentGames(TournamentDetails tournament)
        {
            var endDate = tournament.StartDate.AddMonths(3);
            var gameTimes = tournament.Games?.Select(g => g.Time).ToList();
            return AreGameTimesValid(tournament, endDate, gameTimes);
        }

        private static bool AreGameTimesValid(TournamentDetails tournament, DateTime endDate, List<DateTime>? gameTimes)
        {
            return gameTimes?.All(gt => gt >= tournament.StartDate && gt <= endDate) ?? gameTimes?.Distinct().Count() == gameTimes?.Count;
        }
    }
}