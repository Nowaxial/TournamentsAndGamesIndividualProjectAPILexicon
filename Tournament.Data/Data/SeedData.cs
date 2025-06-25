using Tournament.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tournament.Data.Data
{
    public static class SeedData
    {
        public static async Task Initialize(TournamentContext context)
        {
            await context.Database.MigrateAsync();

            if (!context.TournamentDetails.Any())
            {
                var winterCup = new TournamentDetails
                {
                    Title = "Vintercupen 2025",
                    StartDate = new DateTime(2025, 1, 1),
                    Games =
                    [
                        new () { Title = "Gruppspel", Time = new DateTime(2025, 1, 10) },
                        new () { Title = "Öppningsmatch", Time = new DateTime(2025, 1, 8) },
                        new () { Title = "Final", Time = new DateTime(2025, 1, 16) },
                        new () { Title = "Semifinal", Time = new DateTime(2025, 1, 15) }
                    ]
                };

                var springCup = new TournamentDetails
                {
                    Title = "Vårcupen 2025",
                    StartDate = new DateTime(2025, 3, 1),
                    Games =
                    [
                        new () { Title = "Gruppspel", Time = new DateTime(2025, 4, 10) },
                        new () { Title = "Öppningsmatch", Time = new DateTime(2025, 4, 8) },
                        new () { Title = "Final", Time = new DateTime(2025, 4, 16) },
                        new () { Title = "Semifinal", Time = new DateTime(2025, 4, 15) }
                    ]
                };

                var summerCup = new TournamentDetails
                {
                    Title = "Sommarturneringen",
                    StartDate = new DateTime(2025, 6, 1),
                    Games =
                    [
                        new () { Title = "Gruppspel", Time = new DateTime(2025, 7, 10) },
                        new () { Title = "Öppningsmatch", Time = new DateTime(2025, 7, 8) },
                        new () { Title = "Final", Time = new DateTime(2025, 7, 16) },
                        new () { Title = "Semifinal", Time = new DateTime(2025, 7, 15) }
                    ]
                };
                var autumnCup = new TournamentDetails
                {
                    Title = "Höstturneringen",
                    StartDate = new DateTime(2025, 9, 1),
                    Games =
                    [
                        new () { Title = "Gruppspel", Time = new DateTime(2025, 7, 10) },
                        new () { Title = "Öppningsmatch", Time = new DateTime(2025, 7, 8) },
                        new () { Title = "Final", Time = new DateTime(2025, 7, 16) },
                        new () { Title = "Semifinal", Time = new DateTime(2025, 7, 15) }
                    ]
                };

                context.TournamentDetails.AddRange(winterCup, springCup, summerCup, autumnCup);
                await context.SaveChangesAsync();
            }
        }
    }
}