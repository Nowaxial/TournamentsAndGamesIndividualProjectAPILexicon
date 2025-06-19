using Tournament.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tournament.Data.Data
{
    public static class SeedData
    {
        public static async Task Initialize(TournamentContext context)
        {
            context.Database.EnsureCreated();

            if (!context.TournamentDetails.Any())
            {
                var tournament1 = new TournamentDetails
                {
                    Title = "Vintercupen 2025",
                    StartDate = new DateTime(2025, 1, 15),
                    Games =
                    [
                        new () { Title = "Final", Time = new DateTime(2025, 1, 16) },
                        new () { Title = "Semifinal", Time = new DateTime(2025, 1, 15) }
                    ]
                };

                var tournament2 = new TournamentDetails
                {
                    Title = "Sommarturneringen",
                    StartDate = new DateTime(2025, 6, 10),
                    Games =
                    [
                        new () { Title = "Öppningsmatch", Time = new DateTime(2025, 6, 11) },
                        new () { Title = "Gruppspel", Time = new DateTime(2025, 6, 12) }
                    ]
                };

                context.TournamentDetails.AddRange(tournament1, tournament2);
                await context.SaveChangesAsync();
            }
        }
    }
}